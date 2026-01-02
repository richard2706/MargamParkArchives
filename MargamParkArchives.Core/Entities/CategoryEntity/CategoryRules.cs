using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.CategoryEntity;

public static class CategoryRules
{
    public const int IdMaxLength = 2;
    public const int NameMaxLength = 50;

    public static bool IsValidId(string? id, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, IdMaxLength, propertyName, out error);
    }

    public static bool IsValidName(string? name, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, propertyName, out error);
    }
}
