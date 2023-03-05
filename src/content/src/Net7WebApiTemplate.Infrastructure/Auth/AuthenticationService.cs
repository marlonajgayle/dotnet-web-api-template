using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Features.Authentication.Models;
using Net7WebApiTemplate.Application.Shared.Exceptions;

namespace Net7WebApiTemplate.Infrastructure.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddUserToRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new BadRequestException($"User '{email} was not found.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                throw new BadRequestException($"Role '{roleName}' does not exist.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Failed to add role: {roleName} to user {email}.");
            }
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (roleExist)
            {
                throw new BadRequestException($"Role name '{roleName}' already exists.");
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!roleResult.Succeeded)
            {
                throw new BadRequestException($"Failed to create role '{roleName}'.");
            }
        }

        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usrs = users.Select(u => new AppUser
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToList();

            return usrs;
        }

        public async Task<IEnumerable<string?>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles.Select(role => role.Name).ToList();
        }

        public async Task<IList<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException($"user '{email}' was not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
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

        public async Task RemoveUserFromRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new BadRequestException($"User '{email} was not found.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                throw new BadRequestException($"Role '{roleName}' does not exist.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Failed to remove role '{roleName}' from user '{email}'.");
            }
        }

    }
}