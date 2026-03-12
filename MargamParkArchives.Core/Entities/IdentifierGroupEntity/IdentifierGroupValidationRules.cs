using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.IdentifierGroupEntity;

public static class IdentifierGroupValidationRules
{
    public const int IdentifierGroupIdMaxLength = 3;
    public const int NameMaxLength = 255;

    public static bool IsValidIdentifierGroupId(string? id, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, IdentifierGroupIdMaxLength, propertyName, out error);
    }

    public static bool IsValidName(string? name, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, propertyName, out error);
    }
}
