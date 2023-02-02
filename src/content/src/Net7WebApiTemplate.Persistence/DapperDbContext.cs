using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Persistence
{
    public class DapperDbContext : IDapperDbContext
    {
        private readonly IConfiguration _configuration;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }
    }
}