namespace MargamParkArchives.Core.Entities.ValidationHelpers;

public class ValidationMessages
{
    public const string ValueEmptyMessage = "{0} must contain at least 1 character.";
    public const string ValueTooLongMessage = "{0} must not be longer than {1} characters.";
    public const string EmptyStringIdErrorMessage = "Id cannot be an empty string.";
    public const string InvalidIntIdErrorMessage = "Id cannot be less than 0.";
}
