using System.Data;

namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}