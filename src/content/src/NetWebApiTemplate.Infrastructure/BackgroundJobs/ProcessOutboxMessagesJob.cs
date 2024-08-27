using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Domain.Shared;
using Quartz;
using System.Text.Json;

namespace NetWebApiTemplate.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob(
        IPublisher publisher,
        INetWebApiTemplateDbContext dbContext,       
        ILogger<ProcessOutboxMessagesJob> logger)
        : IJob
    {
        private const int BatchSize = 50;
        private readonly IPublisher _publisher = publisher;
        private readonly INetWebApiTemplateDbContext _dbContext = dbContext;
        private readonly ILogger<ProcessOutboxMessagesJob> _logger = logger;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("ProcessOutboxMessagesJob executing at {UtcNow}.",
                DateTime.UtcNow);

            var unprocessedOutboxMessages = await _dbContext
                .Outboxes
                .Where(o => o.ProcessedAt == null)
                .OrderByDescending(o => o.CreatedAt)
                .Take(BatchSize)
                .ToListAsync(context.CancellationToken);

            foreach (var unprocessedOutboxMessage in unprocessedOutboxMessages)
            {
                try
                {
                    var domainEvent = JsonSerializer
                        .Deserialize<IDomainEvent>(unprocessedOutboxMessage.Payload);

                    if (domainEvent is null)
                    {
                        _logger.LogWarning("Failed to deserialize domain event from outbox message with id:{Id}.",
                            unprocessedOutboxMessage.Id);

                        continue; // move to next item in the list
                    }

                    await _publisher.Publish(domainEvent, context.CancellationToken);
                    unprocessedOutboxMessage.ProcessedAt = DateTime.UtcNow;
                }
                catch (Exception ex) 
                { 
                    unprocessedOutboxMessage.ProcessedAt = DateTime.UtcNow;
                    unprocessedOutboxMessage.Error = ex.Message;

                    _logger.LogCritical(ex, "An exception occurred when processing outbox message Ex: {message}", ex.Message);
                    _dbContext.Outboxes.Update(unprocessedOutboxMessage);
                }

                try
                {
                    await _dbContext.SaveChangesAsync(context.CancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "An exception occurred when saving the state of the outbox Ex: {message}", ex.Message);
                }
            }            
        }
    }
}