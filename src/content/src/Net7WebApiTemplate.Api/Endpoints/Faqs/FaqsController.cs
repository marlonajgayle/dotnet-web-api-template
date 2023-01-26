using Microsoft.AspNetCore.Mvc;

namespace Net7WebApiTemplate.Api.Endpoints.Faq
{
    [Produces("application/json")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        public FaqsController()
        {
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
            return Ok();
        }
    }
}