using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactClassificationRules
{
    public const int ParentIdMaxLength = 50;
    public const int ClassificationTextMaxLength = 255; // Applies to TagsCy, CultureTagEn, LocationCoverage

    public static bool IsValidParentId(string? parentId, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(parentId, ParentIdMaxLength, propertyName, out error);
    }

    public static bool IsValidClassificaionText(string? text, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(text, ClassificationTextMaxLength, propertyName, out error);
    }
}
