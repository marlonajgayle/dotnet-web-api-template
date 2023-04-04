using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Net7WebApiTemplate.Persistence
{
    public static class DbInitializer
    {
        public static IApplicationBuilder UseInitializeDatabase(this IApplicationBuilder application)
        {
            using var serviceScope = application.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<Net7WebApiTemplateDbContext>();

            // only call this method when there are pending migrations
            if (dbContext != null && dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying  Migrations...");
                dbContext.Database.Migrate();
            }

            // TODO: Add method for database seeding
            // SeedData(dbContext);

            return application;
        }
    }
}