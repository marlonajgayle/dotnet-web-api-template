using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest
    {
        public string RoleName { get; set; } = string.Empty;
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public CreateRoleCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.CreateRoleAsync(request.RoleName);
            return Unit.Value;
        }
    }
}
