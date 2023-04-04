using System.Data;

namespace NetWebApiTemplate.Persistence
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}