using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Domain.Entities;
using Net7WebApiTemplate.Domain.Shared;
using Net7WebApiTemplate.Infrastructure.Auth;

namespace Net7WebApiTemplate.Persistence
{
    public class Net7WebApiTemplateDbContext : IdentityDbContext<ApplicationUser>, INet7WebApiTemplateDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatergory> ProductCatergories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Net7WebApiTemplateDbContext(DbContextOptions<Net7WebApiTemplateDbContext> options)
            : base(options)
        {

        }

        public Net7WebApiTemplateDbContext(DbContextOptions<Net7WebApiTemplateDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // applies all configurations defined within the configurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Net7WebApiTemplateDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}