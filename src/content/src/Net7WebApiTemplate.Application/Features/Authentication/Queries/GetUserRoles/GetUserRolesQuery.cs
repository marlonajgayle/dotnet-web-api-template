using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;

namespace Net7WebApiTemplate.Application.Features.Authentication.Queries.GetUserRoles
{
    public record GetUserRolesQuery : IRequest<IList<string>>
    {
        public string Email { get; set; } = string.Empty;
    }

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
    {
        private readonly IAuthenticationService _authenticationService;

        public GetUserRolesQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authenticationService.GetUserRolesAsync(request.Email);

            if (roles == null)
            {
                return new List<string>();
            }

            return roles;
        }
    }
}