using Microsoft.Extensions.DependencyInjection;

namespace Net7WebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            return services;
        }
    }
}