using Microsoft.IdentityModel.JsonWebTokens;
using Net7WebApiTemplate.Application.Shared.Interface;
using System.Security.Claims;

namespace Net7WebApiTemplate.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; } = string.Empty;
        public string Email { get; }
        public bool IsAuthenticated { get; }
        public string IpAddress { get; }


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? "";
            IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
            IsAuthenticated = UserId != null;
        }
    }
}
