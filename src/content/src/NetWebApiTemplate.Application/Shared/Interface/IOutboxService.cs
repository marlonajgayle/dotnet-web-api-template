using NetWebApiTemplate.Domain.Shared;

namespace NetWebApiTemplate.Application.Shared.Interface
{
    public interface IOutboxService
    {
        Task StoreDomainEvent(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}