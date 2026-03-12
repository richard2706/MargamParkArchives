using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.CategoryEntity;

public static class CategoryValidationRules
{
    public const int CategoryIdMaxLength = 2;
    public const int NameMaxLength = 50;

    public static bool IsValidCategoryId(string? id, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, CategoryIdMaxLength, propertyName, out error);
    }

    public static bool IsValidName(string? name, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, propertyName, out error);
    }
}
