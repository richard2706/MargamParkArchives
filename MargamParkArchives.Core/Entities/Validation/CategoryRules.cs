namespace MargamParkArchives.Core.Entities.Validation;

public static class CategoryRules
{
    public const int IdMaxLength = 2;
    public const int NameMaxLength = 50;

    public static bool IsValidId(string id, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, IdMaxLength, nameof(id), out error);
    }

    public static bool IsValidName(string name, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, nameof(name), out error);
    }
}
