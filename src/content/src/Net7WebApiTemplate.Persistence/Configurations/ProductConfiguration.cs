using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.ProductId).HasColumnName("ProductID");

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