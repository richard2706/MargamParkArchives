namespace MargamParkArchives.Core.Entities.ValidationHelpers;

public class ValidationException : Exception
{
    public string PropertyName { get; set; }

    public ValidationException(string message, string propertyName) : base(message)
    {
        PropertyName = propertyName;
    }
}
