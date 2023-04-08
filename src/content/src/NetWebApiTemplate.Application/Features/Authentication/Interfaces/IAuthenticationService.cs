using NetWebApiTemplate.Application.Features.Authentication.Models;
using System.Security.Claims;

namespace NetWebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task AddClaimToUser(string email, string claimName, string claimValue);
        Task AddUserToRoleAsync(string email, string roleName);
        Task CreateRoleAsync(string roleName);
        Task<List<AppUser>> GetAllUsersAsync();
        Task<IEnumerable<string?>> GetRolesAsync();
        Task<IList<Claim>> GetUserClaimsAsync(string email);
        Task<IList<string>> GetUserRolesAsync(string email);
        Task<Result> PasswordSignInAsync(string email, string password, bool LockoutOnFailure);
        Task<Result> RegisterUserAsync(AppUser user, string password);
        Task RemoveUserFromRoleAsync(string email, string roleName);

    }
}