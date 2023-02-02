﻿using Microsoft.EntityFrameworkCore;
using Net7WebApiTemplate.Application.Shared.Interface;
using Net7WebApiTemplate.Domain.Entities;

namespace Net7WebApiTemplate.Persistence
{
    public class Net7WebApiTemplateDbContext : DbContext, INet7WebApiTemplateDbContext
    {
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatergory> ProductCatergories { get; set; }

        public Net7WebApiTemplateDbContext(DbContextOptions<Net7WebApiTemplateDbContext> options)
            : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
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