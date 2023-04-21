using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using __PROJECT_NAME__.Domain.Entities;

namespace __PROJECT_NAME__.Persistence.Configurations
{
    public class __CONFIGURATION_NAME__Configuration : IEntityTypeConfiguration<__CONFIGURATION_NAME__>
    {
        public void Configure(EntityTypeBuilder<__CONFIGURATION_NAME__> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("__CONFIGURATION_NAME__Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Property1)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Property2)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Property3)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}