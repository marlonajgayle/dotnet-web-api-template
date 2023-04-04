using Mediator;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Application.Features.Products.Commands
{
    public record CreateProductCommand : IRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly INetWebApiTemplateDbContext _dbContext;

        public CreateProductCommandHandler(INetWebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var entity = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice
            };

            _dbContext.Products.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}