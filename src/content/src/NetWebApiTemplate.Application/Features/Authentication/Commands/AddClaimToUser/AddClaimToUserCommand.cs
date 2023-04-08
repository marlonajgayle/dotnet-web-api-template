using Mediator;
using NetWebApiTemplate.Application.Features.Authentication.Interfaces;

namespace NetWebApiTemplate.Application.Features.Authentication.Commands.AddClaimToUser
{
    public record AddClaimToUserCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string ClaimName { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
    }

    public class AddClaimToUserCommandHandler : IRequestHandler<AddClaimToUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public AddClaimToUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(AddClaimToUserCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.AddClaimToUser(request.Email, request.ClaimName, request.ClaimValue);
            return Unit.Value;
        }
    }
}
