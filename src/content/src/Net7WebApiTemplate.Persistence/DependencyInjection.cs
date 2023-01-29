using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<Net7WebApiTemplateDbContext>(name: "Application Database");

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
                provider.GetService<Net7WebApiTemplateDbContext>());

            return services;
        }
    }
}