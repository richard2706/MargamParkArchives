using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.CreatorEntity;

public static class CreatorRules
{
    public const int NameMaxLength = 255;

    public static bool IsValidName(string? name, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, propertyName, out error);
    }
}
