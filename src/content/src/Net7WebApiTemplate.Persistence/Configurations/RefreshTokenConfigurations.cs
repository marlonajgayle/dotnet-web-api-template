
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Persistence.Configurations
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("RefreshTokenID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(e => e.JwtId);
            builder.Property(e => e.JwtId)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Token)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .IsRequired();

            builder.Property(e => e.Revoked);
        }
    }
}