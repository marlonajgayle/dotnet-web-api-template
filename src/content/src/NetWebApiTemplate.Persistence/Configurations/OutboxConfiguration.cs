using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Persistence.Configurations
{
    public class OutboxConfiguration : IEntityTypeConfiguration<Outbox>
    {
        public void Configure(EntityTypeBuilder<Outbox> builder)
        {
            builder.HasKey(e => e.Id)
                .IsClustered(false);

            builder.Property(e => e.Id)
                .HasColumnName("OutboxID")
                .ValueGeneratedNever();

            builder.Property(e => e.MessageType)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Payload)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.ProcessedAt);

            builder.Property(e => e.Error)
                .HasMaxLength(500);
        }
    }
}