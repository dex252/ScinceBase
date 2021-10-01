using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Web.Repositories.Connection
{
    public class MySqlDbConnection : IConnection
    {
        private string ConnectionString { get; }

        public MySqlDbConnection(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:MySqlProvider"];
        }

        public IDbConnection OpenConnection()
        {
            var sqlConnection = new MySqlConnection(ConnectionString);

            sqlConnection.Open();

            return sqlConnection;
        }
    }
}
