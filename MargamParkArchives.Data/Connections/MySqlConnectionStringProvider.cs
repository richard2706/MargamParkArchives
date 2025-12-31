using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using Microsoft.Extensions.Options;

namespace MargamParkArchives.Data.Connections;

public class MySqlConnectionStringProvider(IOptions<DatabaseOptions> databaseOptions, IPasswordProvider passwordProvider) : IConnectionStringProvider
{
    private const string ConnectionStringTemplate = "Server={0}; Database={1}; Uid={2}; Pwd={3};";

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;
    private readonly IPasswordProvider _passwordProvider = passwordProvider;

    public async Task<string> GetConnectionStringAsync()
    {
        string password = await _passwordProvider.GetPasswordAsync();
        return string.Format(
            ConnectionStringTemplate,
            _databaseOptions.Server,
            _databaseOptions.Database,
            _databaseOptions.Uid,
            password);
    }
}
