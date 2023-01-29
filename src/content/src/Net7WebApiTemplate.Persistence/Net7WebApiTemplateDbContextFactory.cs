using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Persistence
{
    public class Net7WebApiTemplateDbContextFactory : IDesignTimeDbContextFactory<Net7WebApiTemplateDbContext>
    {
        private const string ConnectionStringName = "DatabaseConnection";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public Net7WebApiTemplateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Net7WebApiTemplateDbContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());

            return new Net7WebApiTemplateDbContext(optionsBuilder.Options);
        }

        private static string GetConnectionString()
        {
            var basePath = Directory.GetCurrentDirectory();

            var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            Console.WriteLine(environmentName);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine(configuration.GetConnectionString(ConnectionStringName));

            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            return connectionString;
        }
    }
}