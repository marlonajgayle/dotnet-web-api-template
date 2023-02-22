using Net7WebApiTemplate.Application.Features.Authentication.Models;

namespace Net7WebApiTemplate.Application.Features.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result> PasswordSignInAsync(string email, string password, bool LockoutOnFailure);
        Task<Result> RegisterUserAsync(AppUser user, string password);
    }
}