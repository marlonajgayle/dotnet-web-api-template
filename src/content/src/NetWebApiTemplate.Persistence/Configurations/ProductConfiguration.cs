using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetWebApiTemplate.Domain.Entities;

namespace NetWebApiTemplate.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId);

            builder.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.ProductPrice)
                .HasColumnType("money")
                .IsRequired();
        }
    }
}