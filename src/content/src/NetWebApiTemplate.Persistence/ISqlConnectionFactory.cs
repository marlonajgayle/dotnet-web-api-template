using System.Data;

namespace NetWebApiTemplate.Application.Shared.Interface
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}