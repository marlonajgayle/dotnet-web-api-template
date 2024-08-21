using FluentValidation;
using HashidsCore.NET;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using NetWebApiTemplate.Application.Features.HealthChecks;
using NetWebApiTemplate.Application.Shared.Behaviours;
using System.Reflection;

namespace NetWebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "NetWebApiTemplate API");

            // Register Fluent Validation service
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register Hash ids service that allows you to hash ids like YouTube
            services.AddSingleton<IHashids>(_ => new Hashids("salt", 11));

            // Register MediatR Services
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(options =>
            {
                
                options.Lifetime = ServiceLifetime.Transient;
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            });

            return services;
        }
    }
}