using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net7WebApiTemplate.Api.Endpoints.Faqs;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.Login;
using Net7WebApiTemplate.Application.Features.Authentication.Commands.RefreshToken;
using Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs;

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
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand()
            {
                AccessToken = request.AccessToken,
                RefreshToken = request.RefreshToken
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
