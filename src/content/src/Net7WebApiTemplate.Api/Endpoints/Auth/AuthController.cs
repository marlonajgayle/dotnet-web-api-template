using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.AddClaimToUser;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.AddUserToRole;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.CreateRole;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.Login;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.RefreshToken;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.RegisterUser;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.RemoveUserFromRole;
using Net7WebApiTemplate.Application.Features.Authentication.Queries.GetAllRoles;
using Net7WebApiTemplate.Application.Features.Authentication.Queries.GetUserClaims;
using Net7WebApiTemplate.Application.Features.Authentication.Queries.GetUserRoles;

namespace Net7WebApiTemplate.Api.Endpoints.Auth
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand()
            {
                Email = request.Email.Trim(),
                Password = request.Password.Trim()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand()
            {
                AccessToken = request.AccessToken.Trim(),
                RefreshToken = request.RefreshToken.Trim()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                Password = request.Password.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var command = new CreateRoleCommand
            {
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var command = new AddUserToRoleCommand
            {
                Email = email.Trim(),
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var query = new GetUserRolesQuery
            {
                Email = email.Trim()
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var command = new RemoveUserFromRoleCommand
            {
                Email = email.Trim(),
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/claims/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserClaims(string email)
        {
            var query = new GetUserClaimsQuery
            {
                Email = email.Trim()
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/claims/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddClaimToUser(string email, string claimName, string claimValue)
        {
            var command = new AddClaimToUserCommand
            {
                Email = email.Trim(),
                ClaimName = claimName.Trim(),
                ClaimValue = claimValue.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }
    }
}