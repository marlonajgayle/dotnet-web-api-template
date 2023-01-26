using Microsoft.Extensions.DependencyInjection;
using Net7WebApiTemplate.Application.Features.HealthChecks;

namespace Net7WebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "Net7WebApiTemplate API");

            return services;
        }
    }
}