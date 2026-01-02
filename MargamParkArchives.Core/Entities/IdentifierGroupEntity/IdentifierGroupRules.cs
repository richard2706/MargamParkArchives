using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.IdentifierGroupEntity;

public static class IdentifierGroupRules
{
    public const int IdMaxLength = 3;
    public const int NameMaxLength = 255;

    public static bool IsValidId(string? id, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(id, IdMaxLength, propertyName, out error);
    }

    public static bool IsValidName(string? name, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, propertyName, out error);
    }
}
