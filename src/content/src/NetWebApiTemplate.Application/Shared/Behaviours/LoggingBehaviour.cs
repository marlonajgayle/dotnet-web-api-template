using Mediator;
using Microsoft.Extensions.Logging;

namespace NetWebApiTemplate.Application.Shared.Behaviours
{
    public class LoggingBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
        where TMessage : IMessage
    {
        private readonly ILogger<TMessage> _logger;

        public LoggingBehaviour(ILogger<TMessage> logger)
        {
            _logger = logger;
        }

        public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
        {
            var requestName = typeof(TMessage).Name;
            _logger.LogInformation("NetWebApiTemplate Request: {name}, {@Request}", requestName, message);

            return await next(message, cancellationToken);
        }
    }
}