using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.GeneralLocationEntity;

public class GeneralLocationRules
{
    private const int NameMaxLength = 255;

    public static bool IsValidName(string? name, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(name, NameMaxLength, nameof(name), out error);
    }
}
