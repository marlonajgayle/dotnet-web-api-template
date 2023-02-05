using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Shared.Exceptions;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResult>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenService _jwtTokenService;
        public LoginCommandHandler(IAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        public async ValueTask<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // validate email and password
            var result = await _authenticationService.PasswordSignInAsync(request.Email, request.Password, false);

            // throw unauthorized exception if credentials validation fails
            if (!result.Succeeded)
            {
                throw new UnauthorizedException("Invalid email and password combination.");
            }

            // Generate JWT token response if validation is successful
            AuthenticationResult response = await _jwtTokenService.GenerateClaimsTokenAsync(request.Email, cancellationToken);

            return response;
        }
    }
}