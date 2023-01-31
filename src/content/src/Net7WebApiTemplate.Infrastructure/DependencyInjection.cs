using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Infrastructure.Cache.InMemory;

namespace Net7WebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            // Register InMemory Cache
            services.AddMemoryCache();
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider> ();

            return services;
        }
    }
}