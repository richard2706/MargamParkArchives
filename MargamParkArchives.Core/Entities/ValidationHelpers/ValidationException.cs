namespace MargamParkArchives.Core.Entities.ValidationHelpers;

public class ValidationException : Exception
{
    public string PropertyName { get; set; }

    /// <summary>
    /// Validation exception for a single invalid value.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="propertyName"></param>
    public ValidationException(string message, string propertyName) : base(message)
    {
        PropertyName = propertyName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ValidationException(string message) : base(message)
    {
        PropertyName = string.Empty;
    }
}
