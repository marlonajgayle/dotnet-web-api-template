using FluentValidation;
using HashidsCore.NET;
using Mediator;
using Microsoft.AspNetCore.Hosting;
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

            // Register Fluent Validation serivce
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register Hash ids service that allows you to hash ids like youtube
            services.AddSingleton<IHashids>(_ => new Hashids("salt", 11));

            // Register MediatR Services
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IMediator).Assembly));


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;
        }
    }
}