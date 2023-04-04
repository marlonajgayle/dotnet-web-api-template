using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Persistence.Configurations
{
    public class FaqConfiguration : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("FaqId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Question)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Answer)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}