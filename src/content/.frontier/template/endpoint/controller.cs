using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace __PROJECT_NAME__.Api.Endpoints.__ENDPOINT_NAME__
{
    [Produces("application/json")]
    [ApiController]
    public class __ENDPOINT_NAME__ : ControllerBase
    {
        private readonly IMediator _mediator;

        public __ENDPOINT_NAME__(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  This is a simple GET request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/__ENDPOINT_NAME_LOWER__/sample")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Sample(SampleRequest request)
        {
            Console.WriteLine($"Property1: {request.Property1}");
            Console.WriteLine($"Property2: {request.Property2}");

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}