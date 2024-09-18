using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetWebApiTemplate.Application.Shared.Interface;
using NetWebApiTemplate.Domain.Entities;
using NetWebApiTemplate.Domain.Shared;
using NetWebApiTemplate.Infrastructure.Auth;

namespace NetWebApiTemplate.Persistence
{
    public class NetWebApiTemplateDbContext : IdentityDbContext<ApplicationUser>, INetWebApiTemplateDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatergory> ProductCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public NetWebApiTemplateDbContext(DbContextOptions<NetWebApiTemplateDbContext> options)
            : base(options)
        {

        }

        public NetWebApiTemplateDbContext(DbContextOptions<NetWebApiTemplateDbContext> options,
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
                        entry.Entity.CreatedBy = _currentUserService?.UserId ?? string.Empty;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService?.UserId ?? string.Empty;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // applies all configurations defined within the configurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetWebApiTemplateDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}