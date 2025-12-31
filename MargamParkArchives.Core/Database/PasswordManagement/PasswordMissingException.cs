namespace MargamParkArchives.Core.Database.PasswordManagement;

public class PasswordMissingException : Exception
{
    public PasswordMissingException() : base()
    {
    }

    public PasswordMissingException(string message) : base(message)
    {
    }

    public PasswordMissingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
