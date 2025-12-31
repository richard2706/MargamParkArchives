namespace MargamParkArchives.Core.Database.PasswordManagement.Storage;

/// <summary>
/// Response object for database password storage attempts. The Result property indicates if the
/// password was stored successfully or the type of error that occurred.
/// </summary>
public class PasswordStorageResponse
{
    public PasswordStorageResult Result { get; init; }
    public Exception? ExceptionThrown { get; init; }

    public PasswordStorageResponse(PasswordStorageResult result)
    {
        Result = result;
        ExceptionThrown = null;
    }

    public PasswordStorageResponse(PasswordStorageResult result, Exception? exceptionThrown)
    {
        Result = result;
        ExceptionThrown = exceptionThrown;
    }
}
