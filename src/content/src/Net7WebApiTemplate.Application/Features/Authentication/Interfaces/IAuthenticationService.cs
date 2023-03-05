using Net7WebApiTemplate.Application.Features.Authentication.Models;
using Net7WebApiTemplate.Application.Shared.Models;

namespace Net7WebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task AddUserToRoleAsync(string email, string roleName);
        Task CreateRoleAsync(string roleName);
        Task<List<AppUser>> GetAllUsersAsync();
        Task<IEnumerable<string?>> GetRolesAsync();
        Task<IList<string>> GetUserRolesAsync(string email);
        Task<Result> PasswordSignInAsync(string email, string password, bool LockoutOnFailure);
        Task<Result> RegisterUserAsync(AppUser user, string password);
        Task RemoveUserFromRoleAsync(string email, string roleName);
        
    }
}