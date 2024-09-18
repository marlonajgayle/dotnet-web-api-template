
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetWebApiTemplate.Api.Endpoints.Faqs;
using NetWebApiTemplate.Application.Features.Faqs.Queries.GetAllFaqs;

namespace NetWebApiTemplate.Api.Endpoints.Faq
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
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(FaqRequest request)
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
    }
}