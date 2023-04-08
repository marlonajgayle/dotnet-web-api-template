using Mediator;
using NetWebApiTemplate.Application.Features.Products.Interfaces;
using NetWebApiTemplate.Application.Shared.Exceptions;

namespace NetWebApiTemplate.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : IRequest
    {
        public long Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async ValueTask<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var entity = await _productRepository.GetById(request.Id);

            // check if product was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Products), request.Id);
            }

            // update product record
            await _productRepository.Update(entity);

            return Unit.Value;

        }
    }
}