using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Net7WebApiTemplate.Application.Features.HealthChecks;
using Net7WebApiTemplate.Application.Shared.Behaviours;
using System.Reflection;

namespace Net7WebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "Net7WebApiTemplate API");

            // Register Fluent Validation serivce
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register MediatR Services
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediator();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour <,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;
        }
    }
}