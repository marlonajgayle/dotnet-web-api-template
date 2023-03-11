using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;

namespace Net7WebApiTemplate.Application.Features.Authentication.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<IEnumerable<string?>>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<string?>>
    {
        private readonly IAuthenticationService _authenticationService;

        public GetAllRolesQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<IEnumerable<string?>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authenticationService.GetRolesAsync();

            if (roles == null)
            {
                return Enumerable.Empty<string>();
            }

            return roles;
        }
    }
}