using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net7WebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs;

namespace Net7WebApiTemplate.Api.Endpoints.Faq
{
    [Produces("application/json")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaqsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Retrieves all FAQ information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllFaqsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}