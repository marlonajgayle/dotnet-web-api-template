# Create API Endpoint

.NET Web API Solution Template provides a structured way to create API endpoints that group all the necessary files together making it 
easier to create and update related classes.

Navigate to the `Endpoints` folder within the ProjectName.API project and create your endpoint directory.

In this example we will be creating a Faqs endpoint.
```
 cd ProjectName.API/Endpoints
```

Then create our controller class `FaqController.cs` and add the following:
* ApiController Annotation `[ApiController]`
* Private readonly _mediator property
* FaqController constructor with dependency injection of IMediator

```
[Produces("application/json")]
[ApiController]
public class FaqsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FaqsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    ...
}
```

Create endpoint method `GetAll` with the following:
* Add Http verb [HttpPost]
* Add API version [ApiVersion("1.0")]
* Add API route [Route("api/v{version:apiVersion}/faqs")]
* Add Command/Query logic, please see create Command/Query

```
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
```

