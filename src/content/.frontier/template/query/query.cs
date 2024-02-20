using Mediator;
// using __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Interfaces;

namespace __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Queries.__QUERY_NAME__
{
    public record __QUERY_NAME__Query : IRequest<string?>
    {
    }

    public class __QUERY_NAME__QueryHandler : IRequestHandler<__QUERY_NAME__Query, string?>
    {
        // private readonly IAuthenticationService _authenticationService;

        public __QUERY_NAME__QueryHandler(/*IAuthenticationService authenticationService*/)
        {
            // _authenticationService = authenticationService;
        }

        public async ValueTask<string?> Handle(__QUERY_NAME__Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}