using System.Security.Claims;

namespace Net7WebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IJwtTokenService
    {
        Task<AuthenticationResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken);
        Task<ClaimsPrincipal> GetPrincipFromTokenAsync(string token);
    }
}