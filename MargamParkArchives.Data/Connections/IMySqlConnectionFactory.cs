using MySqlConnector;

namespace MargamParkArchives.Data.Connections
{
    public interface IMySqlConnectionFactory
    {
        MySqlConnection CreateConnection();
    }
}