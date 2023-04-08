using Mediator;
using NetWebApiTemplate.Application.Features.Authentication.Interfaces;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.AddUserToRole
{
    public record AddUserToRoleCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }

    public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public AddUserToRoleCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.AddUserToRoleAsync(request.Email, request.RoleName);

            return Unit.Value;
        }
    }
}
