using System.Data;

namespace Net7WebApiTemplate.Persistence
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}