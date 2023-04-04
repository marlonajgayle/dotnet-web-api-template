using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetWebApiTemplate.Application.Features.Faqs.Interfaces;
using NetWebApiTemplate.Application.Features.Products.Interfaces;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Persistence.Repositories;

namespace NetWebApiTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<NetWebApiTemplateDbContext>(name: "Application Database");

            // Register Dapper DbContext and Repositories
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();

            if (environment.IsProduction())
            {
                services.AddDbContext<NetWebApiTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly(typeof(NetWebApiTemplateDbContext).Assembly.FullName)));
            }
            else
            {
                services.AddDbContext<NetWebApiTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly(typeof(NetWebApiTemplateDbContext).Assembly.FullName))
                .LogTo(Console.WriteLine, LogLevel.Information));
            }

            services.AddScoped<INetWebApiTemplateDbContext>(provider =>
                provider.GetRequiredService<NetWebApiTemplateDbContext>());

            return services;
        }
    }
}