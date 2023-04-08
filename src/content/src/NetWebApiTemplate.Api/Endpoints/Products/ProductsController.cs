using Mediator;
using Microsoft.AspNetCore.Mvc;
using NetWebApiTemplate.Application.Features.Products.Commands;
using NetWebApiTemplate.Application.Features.Products.Commands.DeleteProduct;
using NetWebApiTemplate.Application.Features.Products.Commands.UpdateProduct;
using NetWebApiTemplate.Application.Features.Products.Queries.GetAllProducts;

namespace NetWebApiTemplate.Api.Endpoints.Products
{
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var command = new CreateProductCommand()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.Price
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand()
            {
                Id = request.ProductId,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.Price
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}