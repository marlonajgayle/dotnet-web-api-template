using Microsoft.AspNetCore.Identity;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Features.Authentication.Models;

namespace Net7WebApiTemplate.Infrastructure.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> PasswordSignInAsync(string email, string password, bool LockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, LockoutOnFailure);

            if (result.IsLockedOut)
            {
                return Result.Failure(new string[] { "Account locked, too many invalid login attempts." });
            }

            return result.MapToResult();
        }
    }
}