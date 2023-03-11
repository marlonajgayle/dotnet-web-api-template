using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;

namespace Net7WebApiTemplate.Application.Features.Authentication.Queries.GetUserClaims
{
    public record GetUserClaimsQuery : IRequest<List<string>>
    {
        public string Email { get; set; } = string.Empty;
    }

    public class GetUserClaimsQueryHandler : IRequestHandler<GetUserClaimsQuery, List<string>>
    {
        private readonly IAuthenticationService _authenticationService;

        public GetUserClaimsQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<List<string>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var userClaims = await _authenticationService.GetUserClaimsAsync(request.Email);

            return userClaims.Select(c => c.Value).ToList();
        }
    }
}