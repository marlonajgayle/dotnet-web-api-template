using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Net7WebApiTemplate.Application.Shared.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Net7WebApiTemplate Request: {name}, {@Request}", requestName, request);

            return Task.CompletedTask;
        }
    }
}