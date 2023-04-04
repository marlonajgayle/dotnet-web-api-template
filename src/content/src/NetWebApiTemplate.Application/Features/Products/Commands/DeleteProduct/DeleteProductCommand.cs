using Mediator;
using NetWebApiTemplate.Application.Shared.Exceptions;
using NetWebApiTemplate.Application.Shared.Interface;

namespace NetWebApiTemplate.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly INetWebApiTemplateDbContext _dbContext;

        public DeleteProductCommandHandler(INetWebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Products.FirstOrDefault(p => p.ProductId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}