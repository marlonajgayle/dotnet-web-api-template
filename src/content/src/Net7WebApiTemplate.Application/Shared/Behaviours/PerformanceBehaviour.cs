using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Net7WebApiTemplate.Application.Shared.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer= new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            // any request taking longer than 2 seconds should be reported
            if(elapsedMilliseconds > 2000) 
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("Net7WebApiTemplate long running request: {name} ({elapsedMilliseconds} milliseconds) {@Request}]",
                    requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}