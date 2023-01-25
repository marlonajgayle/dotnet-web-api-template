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
        [Route("api/v1/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }
    }
}