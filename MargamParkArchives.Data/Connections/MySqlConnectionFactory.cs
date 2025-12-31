using MargamParkArchives.Core.Database;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

/// <summary>
/// Class of static methods for database operations used thoughout the application.
/// </summary>
public class MySqlConnectionFactory(IConnectionStringProvider connectionStringProvider) : IMySqlConnectionFactory
{
    private readonly IConnectionStringProvider _connectionStringProvider = connectionStringProvider;

    public async Task<MySqlConnection> CreateConnection()
    {
        string connectionString = await _connectionStringProvider.GetConnectionString();
        return new MySqlConnection(connectionString);
    }
}
