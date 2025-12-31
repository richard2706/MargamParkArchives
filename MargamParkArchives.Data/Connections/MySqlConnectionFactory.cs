using MargamParkArchives.Core.Database;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

/// <summary>
/// Class of static methods for database operations used thoughout the application.
/// </summary>
public class MySqlConnectionFactory(IConnectionStringProvider connectionStringProvider) : IMySqlConnectionFactory
{
    private readonly IConnectionStringProvider _connectionStringProvider = connectionStringProvider;

    public async Task<MySqlConnection> CreateConnectionAsync()
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        return new MySqlConnection(connectionString);
    }
}
