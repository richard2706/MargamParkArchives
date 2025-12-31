using MySqlConnector;

namespace MargamParkArchives.Data.Connections
{
    public interface IMySqlConnectionFactory
    {
        public Task<MySqlConnection> CreateConnectionAsync();
    }
}