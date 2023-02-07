using Net7WebApiTemplate.Application.Features.Authentication.Models;
using System.Security.Claims;

namespace Net7WebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IJwtTokenService
    {
        Task<TokenResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken);
        Task<ClaimsPrincipal?> GetPrincipFromTokenAsync(string token);
        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
    }
}