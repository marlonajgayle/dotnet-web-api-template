using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Infrastructure.ApiClients.GitHub;
using Net7WebApiTemplate.Infrastructure.Cache.InMemory;
using Net7WebApiTemplate.Infrastructure.DataProtection;
using Net7WebApiTemplate.Infrastructure.Email;
using Polly;
using System.Net;
using System.Net.Mail;

namespace Net7WebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            //
            // Register Data Protection
            services.AddDataProtection();
            services.AddSingleton<IEncryptionManager, EncryptionManager>();

            // Register Email Sender and Configurations
            var emailSenderOptions = new EmailSenderOptions();
            configuration.GetSection(nameof(EmailSenderOptions)).Bind(emailSenderOptions);
            services.AddScoped<IEmailSender, EmailSenderService>();

            services.AddFluentEmail(defaultFromEmail: emailSenderOptions.FromEmail)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient(emailSenderOptions.Host, emailSenderOptions.Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network ,
                    Credentials = (emailSenderOptions.RequiresAuthentication) ? 
                    new NetworkCredential(emailSenderOptions.Username, emailSenderOptions.Password) : null,
                    EnableSsl = emailSenderOptions.EnableSsl
                });

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

            return services;
        }
    }
}