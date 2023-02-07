using Mediator;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Shared.Exceptions;

namespace Net7WebApiTemplate.Application.Features.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand : IRequest<AuthenticationResult>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResult>
    {
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public async ValueTask<AuthenticationResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _jwtTokenService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);

            if(!result.Succeeded) 
            {
                throw new UnauthorizedException(result.Error);
            }

            return new AuthenticationResult 
            { 
                AccessToken = result.AccessToken,
                TokenType = "Bearer",
                ExpiresIn = result.ExpiresIn,
                RefreshToken = result.RefreshToken
            };
        }
    }
}