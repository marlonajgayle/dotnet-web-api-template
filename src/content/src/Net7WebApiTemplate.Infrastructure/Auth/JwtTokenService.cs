using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Net7WebApiTemplate.Application.Features.Authentication.Interfaces;
using Net7WebApiTemplate.Application.Features.Authentication.Models;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net7WebApiTemplate.Infrastructure.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly INet7WebApiTemplateDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtTokenService(JwtOptions jwtOptions, INet7WebApiTemplateDbContext dbContext,
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            TokenValidationParameters tokenValidationParameters)
        {
            _jwtOptions = jwtOptions;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<TokenResult> GenerateClaimsTokenAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new TokenResult { Succeeded = false, Error = "User provided does not exist." };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = await GetUserClaims(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtOptions.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create JWT tokens
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // generate and save refresh token
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                Token = GenerateRefreshToken(),
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMonths(3),
                UserId = user.Id
            };

            // Save generated refresh token to database
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new TokenResult
            {
                Succeeded = true,
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = _jwtOptions.Expiration.Seconds,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<ClaimsPrincipal?> GetPrincipFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // disable token lifetime validation as we are validating against an expired token.
                var tokenValdationParams = _tokenValidationParameters.Clone();
                tokenValdationParams.ValidateLifetime = false;

                var principal = tokenHandler.ValidateToken(token, tokenValdationParams, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return await Task.Run(() => principal);
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user?.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddMinutes(_jwtOptions.Expiration.TotalMinutes)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // getting the claims we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // get the user roles and add it to the claims
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private static string GenerateRefreshToken()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(chars.Length)]).ToArray());
        }

        public async Task<TokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
        {
            var validatedToken = await GetPrincipFromTokenAsync(token);

            if (validatedToken == null)
            {
                return new TokenResult { Succeeded = false, Error = "Invalid token" };
            }

            var expirationDate = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirationDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expirationDate);

            if (expirationDateTimeUtc > DateTime.UtcNow)
            {
                return new TokenResult { Succeeded = false, Error = "This access token hasn't expired" };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new TokenResult { Succeeded = false, Error = "This access token does not exist" };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token has expired" };
            }

            if (!storedRefreshToken.IsActive)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token has already been used" };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token does not match this JWT" };
            }

            storedRefreshToken.Revoked = DateTime.UtcNow;
            _dbContext.RefreshTokens.Update(storedRefreshToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var tokenResult = await GenerateClaimsTokenAsync(validatedToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value, cancellationToken);

            return tokenResult;
        }
    }
}