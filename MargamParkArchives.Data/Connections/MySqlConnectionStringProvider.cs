using MargamParkArchives.Core;
using Microsoft.Extensions.Options;

namespace MargamParkArchives.Data.Connections;

public class MySqlConnectionStringProvider(IOptions<DatabaseOptions> databaseOptions, IPasswordProvider passwordProvider) : IConnectionStringProvider
{
    private const string ConnectionStringTemplate = "Server={0}; Database={1}; Uid={2}; Pwd={3};";

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;

    public string GetConnectionString()
    {
        string password = _passwordProvider.GetPassword();
        return string.Format(
            ConnectionStringTemplate,
            _databaseOptions.Server,
            _databaseOptions.Database,
            _databaseOptions.Uid,
            password);
    }
}
