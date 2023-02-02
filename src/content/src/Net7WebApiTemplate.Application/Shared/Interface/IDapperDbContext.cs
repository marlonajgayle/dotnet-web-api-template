using Microsoft.Data.SqlClient;

namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface IDapperDbContext
    {
        SqlConnection CreateConnection();
    }
}