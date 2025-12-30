namespace MargamParkArchives.Core;

/// <summary>
/// Response object for database password validation attempts. The ValidationResult property indicates is the
/// password was correct, incorrect or if a different error occurred.
/// </summary>
public class PasswordValidationResponse
{
    public PasswordValidationResult ValidationResult { get; init; }
    public Exception? exceptionThrown { get; init; }

    public PasswordValidationResponse(PasswordValidationResult validationResult)
    {
        ValidationResult = validationResult;
        exceptionThrown = null;
    }

    public PasswordValidationResponse(PasswordValidationResult validationResult, Exception? databaseException)
    {
        ValidationResult = validationResult;
        exceptionThrown = databaseException;
    }
}
