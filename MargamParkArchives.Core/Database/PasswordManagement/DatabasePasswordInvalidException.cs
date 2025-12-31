namespace MargamParkArchives.Core.Database.PasswordManagement;

public class DatabasePasswordInvalidException : Exception
{
    public DatabasePasswordInvalidException() : base() { }

    public DatabasePasswordInvalidException(string message) : base(message) { }

    public DatabasePasswordInvalidException(string message, Exception innerException) : base(message, innerException) { }
}
