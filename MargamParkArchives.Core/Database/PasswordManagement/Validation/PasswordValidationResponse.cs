namespace MargamParkArchives.Core.Database.PasswordManagement.Validation;

/// <summary>
/// Response object for database password validation attempts. The ValidationResult property indicates is the
/// password was correct, incorrect or if a different error occurred.
/// </summary>
public class PasswordValidationResponse
{
    public PasswordValidationResult Result { get; init; }
    public Exception? ExceptionThrown { get; init; }

    public PasswordValidationResponse(PasswordValidationResult validationResult)
    {
        Result = validationResult;
        ExceptionThrown = null;
    }

    public PasswordValidationResponse(PasswordValidationResult validationResult, Exception? databaseException)
    {
        Result = validationResult;
        ExceptionThrown = databaseException;
    }
}
