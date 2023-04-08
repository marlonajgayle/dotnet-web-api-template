using NetWebApiTemplate.Application.Features.Authentication.Models;
using System.Security.Claims;

namespace NetWebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IJwtTokenService
    {
        Task<TokenResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken);
        Task<ClaimsPrincipal?> GetPrincipFromTokenAsync(string token);
        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
    }
}