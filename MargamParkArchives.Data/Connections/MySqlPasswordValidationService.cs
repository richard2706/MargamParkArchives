using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

public class MySqlPasswordValidationService(IOptions<DatabaseOptions> databaseOptions) : IDatabasePasswordValidationService
{
    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;

    public async Task<PasswordValidationResponse> ValidatePasswordAsync(string password)
    {
        DirectPasswordProvider passwordProvider = new(password);
        MySqlConnectionStringProvider connectionStringProvider = new(databaseOptions, passwordProvider);
        string connectionString = connectionStringProvider.GetConnectionString();
        await using MySqlConnection connection = new(connectionString);
        try
        {
            await connection.OpenAsync();
            // If we reach here, the password is correct
            return new PasswordValidationResponse(PasswordValidationResult.Correct);
        }
        catch (MySqlException ex)
        {
            return ex.SqlState switch
            {
                // Invalid authorization specification
                "28000" => new PasswordValidationResponse(PasswordValidationResult.Incorrect, ex),

                // Communication link failure (i.e. server connection issue)
                "08S01" => new PasswordValidationResponse(PasswordValidationResult.ServerUnreachable, ex),

                // Other exception
                _ => new PasswordValidationResponse(PasswordValidationResult.OtherError, ex),
            };
        }
    }
}
