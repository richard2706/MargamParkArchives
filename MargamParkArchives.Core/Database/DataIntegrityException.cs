namespace MargamParkArchives.Core.Database;

public class DataIntegrityException : DatabaseException
{
    public DataIntegrityException() { }
    public DataIntegrityException(string message) : base(message) { }
    public DataIntegrityException(string? message, Exception? innerException) : base(message, innerException) { }
}
