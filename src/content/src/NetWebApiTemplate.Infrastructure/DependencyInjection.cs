using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetWebApiTemplate.Application.Features.Authentication.Interfaces;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Infrastructure.ApiClients.GitHub;
using NetWebApiTemplate.Infrastructure.Auth;
using NetWebApiTemplate.Infrastructure.BackgroundJobs;
using NetWebApiTemplate.Infrastructure.Cache.InMemory;
using NetWebApiTemplate.Infrastructure.DataProtection;
using NetWebApiTemplate.Infrastructure.Email;
using Polly;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NetWebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            // Configure Quartz scheduler for background Job
            services.AddQuartz(options =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                options.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(3)
                            .RepeatForever()));
            });

            // configure Quartz to wait to jobs to complete
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            // Register Data Protection
            services.AddDataProtection();
            services.AddSingleton<IEncryptionManager, EncryptionManager>();

            // Register Email Sender and Configurations
            var emailSenderOptions = new EmailSenderOptions();
            configuration.GetSection(nameof(EmailSenderOptions)).Bind(emailSenderOptions);
            services.AddSingleton(emailSenderOptions);

            services.AddFluentEmail(defaultFromEmail: emailSenderOptions.FromEmail)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient(emailSenderOptions.Host, emailSenderOptions.Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = (emailSenderOptions.RequiresAuthentication) ?
                    new NetworkCredential(emailSenderOptions.Username, emailSenderOptions.Password) : null,
                    EnableSsl = emailSenderOptions.EnableSsl
                });

            services.AddScoped<IEmailSender, EmailSenderService>();

            // Register Http Client
            services.AddHttpClient(name: "GitHub", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add(name: "Accept", value: "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "HttpClientFactoryExample");
            })
                // Polly retry policy that is configgured to handle errors (HTTP 5XX, HTTP 408)
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(300)));

            // Register GitHub API Service
            services.AddScoped<IGitHubService, GitHubApiService>();

            // Register InMemory Cache
            services.AddMemoryCache();
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

            // Register Authentication Service
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Configure JWT Authentication and Authorization
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            var jwtOptions = new JwtOptions();
            configuration.Bind(nameof(JwtOptions), jwtOptions);
            services.AddSingleton(jwtOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = jwtOptions.ValidateIssuer,
                ValidateAudience = jwtOptions.ValidateAudience,
                ValidAudience = jwtOptions.Audience,
                RequireExpirationTime = jwtOptions.RequireExpirationTime,
                ValidateLifetime = jwtOptions.ValidateLifetime,
                ClockSkew = jwtOptions.Expiration
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            // Register Identity DbContext and Server
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            var identityOptionsConfig = new IdentityOptionsConfig();
            configuration.GetSection(nameof(IdentityOptions)).Bind(identityOptionsConfig);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = identityOptionsConfig.RequiredLength;
                options.Password.RequireDigit = identityOptionsConfig.RequiredDigit;
                options.Password.RequireLowercase = identityOptionsConfig.RequireLowercase;
                options.Password.RequiredUniqueChars = identityOptionsConfig.RequiredUniqueChars;
                options.Password.RequireUppercase = identityOptionsConfig.RequireUppercase;
                options.Lockout.MaxFailedAccessAttempts = identityOptionsConfig.MaxFailedAttempts;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(identityOptionsConfig.LockoutTimeSpanInDays);
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            return services;
        }
    }
}