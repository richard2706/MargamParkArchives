using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactRightsInformationRules
{
    public const int RightsInformationMaxLength = 255;

    public static bool IsValidRightsInformation(string? information, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(information, RightsInformationMaxLength, propertyName, out error);
    }
}
