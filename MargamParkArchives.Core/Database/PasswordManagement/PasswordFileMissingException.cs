namespace MargamParkArchives.Core.Database.PasswordManagement;

public class PasswordFileMissingException : Exception
{
    public PasswordFileMissingException() : base()
    {
    }

    public PasswordFileMissingException(string message) : base(message)
    {
    }

    public PasswordFileMissingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
