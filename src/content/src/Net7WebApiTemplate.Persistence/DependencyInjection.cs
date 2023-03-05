using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Net7WebApiTemplate.Application.Features.Faqs.Interfaces;
using Net7WebApiTemplate.Application.Features.Products.Interfaces;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Persistence.Repositories;

namespace Net7WebApiTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<Net7WebApiTemplateDbContext>(name: "Application Database");

            // Register Dapper DbContext and Repositories
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();

            if (environment.IsProduction())
            {
                services.AddDbContext<Net7WebApiTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly(typeof(Net7WebApiTemplateDbContext).Assembly.FullName)));
            }
            else
            {
                services.AddDbContext<Net7WebApiTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly(typeof(Net7WebApiTemplateDbContext).Assembly.FullName))
                .LogTo(Console.WriteLine, LogLevel.Information));
            }

            services.AddScoped<INet7WebApiTemplateDbContext>(provider =>
                provider.GetRequiredService<Net7WebApiTemplateDbContext>());

            return services;
        }
    }
}