using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Services;

/// <summary>
/// 
/// </summary>
/// <param name="dataAccess"></param>
public class DatabaseConnectionCheckService(IMySqlDataAccess dataAccess)
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    /// <summary>
    /// Checks if a password prompt is required for the database connection
    /// </summary>
    /// <returns>True if a password prompt is required, false otherwise</returns>
    public async Task<bool> IsStoredPasswordValidAsync()
    {
        try
        {
            // Attempt to execute a simple query to check the connection
            await _dataAccess.ExistsAsync("SELECT 1");
            return true; // Connection successful, stored password is valid
        }
        catch (Exception ex) when (ex is PasswordFileMissingException or DatabasePasswordInvalidException)
        {
            return false; // Connection failed due to invalid/missing password
        }
        catch (Exception)
        {
            throw; // Rethrow any other exceptions for further handling
        }
    }
}
