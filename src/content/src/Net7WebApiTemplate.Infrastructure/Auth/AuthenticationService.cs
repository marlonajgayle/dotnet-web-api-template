using Microsoft.AspNetCore.Identity;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Features.Authentication.Models;

namespace Net7WebApiTemplate.Infrastructure.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

        public async Task<Result> RegisterUserAsync(AppUser user, string password)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var result = await _userManager.CreateAsync(appUser, password);

            // add require role(s)

            return result.MapToResult();
        }
    }
}