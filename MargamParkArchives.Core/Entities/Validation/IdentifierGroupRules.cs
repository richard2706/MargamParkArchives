namespace MargamParkArchives.Core.Entities.Validation;

public static class IdentifierGroupRules
{
    private const int IdMaxLength = 3;
    private const int NameMaxLength = 255;

    public static bool IsValidId(string id, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, IdMaxLength, nameof(id), out error);
    }

    public static bool IsValidName(string name, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, nameof(name), out error);
    }
}
