using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Features.Authentication.Models;
using Net7WebApiTemplate.Application.Shared.Exceptions;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async ValueTask<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var result = await _authenticationService.RegisterUserAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("failed to register user.");
            }

            return Unit.Value;
        }
    }
}