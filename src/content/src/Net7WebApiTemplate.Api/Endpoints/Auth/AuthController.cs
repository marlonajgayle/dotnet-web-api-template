using Mediator;
using Microsoft.AspNetCore.Mvc;
using Net7WebApiTemplate.Api.Endpoints.Faqs;
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
        public async Task<IActionResult> Login(FaqRequest request)
        {
            var query = new GetAllFaqsQuery()
            {
                SearchTerm = request.SearchTerm,
                Offset = request.Offset,
                Limit = request.Limit,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
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
