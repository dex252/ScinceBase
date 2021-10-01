using System.Data;
namespace Web.Repositories.Connection
{
    public interface IConnection
    {
        IDbConnection OpenConnection();
    }
}
