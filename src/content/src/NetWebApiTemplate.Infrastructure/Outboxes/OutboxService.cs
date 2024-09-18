using Microsoft.Extensions.Logging;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Domain.Entities;
using NetWebApiTemplate.Domain.Shared;
using Newtonsoft.Json;

namespace NetWebApiTemplate.Infrastructure.Outboxes
{
    public class OutboxService(INetWebApiTemplateDbContext dbContext, 
        ILogger<OutboxService> logger) 
        : IOutboxService
    {
        private readonly INetWebApiTemplateDbContext _dbContext = dbContext;
        private readonly ILogger<OutboxService> _logger = logger;

        public async Task StoreDomainEvent(IDomainEvent domainEvent, 
            CancellationToken cancellationToken)
        {
            await _dbContext.OutboxMessages.AddAsync(new OutboxMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                MessageType = domainEvent.GetType().Name,
                Payload = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            }, cancellationToken);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save domain event to outbox: {Message}", ex.Message);
                throw;
            }
        }
    }
}