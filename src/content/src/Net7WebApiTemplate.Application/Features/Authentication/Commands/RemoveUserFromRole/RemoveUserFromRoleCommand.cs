using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.RemoveUserFromRole
{
    public record RemoveUserFromRoleCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }

    public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public RemoveUserFromRoleCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.RemoveUserFromRoleAsync(request.Email, request.RoleName);

            return Unit.Value;
        }
    }
}