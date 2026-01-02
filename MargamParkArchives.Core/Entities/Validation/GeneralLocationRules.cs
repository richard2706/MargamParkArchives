namespace MargamParkArchives.Core.Entities.Validation;

public class GeneralLocationRules
{
    private const int NameMaxLength = 255;

    public static bool IsValidName(string name, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, nameof(name), out error);
    }
}
