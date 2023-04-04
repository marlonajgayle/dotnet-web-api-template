using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCatergory>
    {
        public void Configure(EntityTypeBuilder<ProductCatergory> builder)
        {
            builder.HasKey(e => e.CategoryId);

            builder.Property(e => e.CategoryId)
                .HasColumnName("CategoryID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CategoryName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.CategoryDescription)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}