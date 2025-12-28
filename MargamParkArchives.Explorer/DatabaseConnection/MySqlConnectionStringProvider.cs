using MargamParkArchives.Data.Connections;
using Microsoft.Extensions.Options;

namespace MargamParkArchives.Explorer.DatabaseConnection;

internal class MySqlConnectionStringProvider(IOptions<DatabaseOptions> databaseOptions) : IConnectionStringProvider
{
    private const string ConnectionStringTemplate = "Server={0}; Database={1}; Uid={2}; Pwd={3};";

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;

    public string GetConnectionString()
    {
        string password = "";
        return string.Format(
            ConnectionStringTemplate,
            _databaseOptions.Server,
            _databaseOptions.Database,
            _databaseOptions.Uid,
            password);
    }
}
