

using MediatR;
using Microsoft.Extensions.Logging;

namespace Net7WebApiTemplate.Application.Shared.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try 
            {
                return await next();
            }
            catch (Exception ex) 
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "Net7WebApiTemplate Request: Unhandled Exception for request {Name} {@Request}",
                    requestName, request);

                throw;
            }
        }
    }
}