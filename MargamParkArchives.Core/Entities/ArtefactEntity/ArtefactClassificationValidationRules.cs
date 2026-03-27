using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactClassificationValidationRules
{
    public const int ParentIdMaxLength = 50;
    public const int ClassificationTextMaxLength = 255; // Applies to TagsCy, CultureTagEn, LocationCoverage

    /// <summary>
    /// Returns true if the string value provided is a valid ParentId based on maximum length constraints.
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is a valid ParentId</returns>
    public static bool IsValidParentId(string? parentId, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(parentId, ParentIdMaxLength, propertyName, out error);
    }

    /// <summary>
    /// Returns true if the string value provided is valid as classification text based on maximum length constraints.
    /// </summary>
    /// <remarks>Classification properties are tagsCy, cultureTagEn, locationCoverage</remarks>
    /// <param name="parentId"></param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is a valid ParentId</returns>
    public static bool IsValidClassificaionText(string? text, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(text, ClassificationTextMaxLength, propertyName, out error);
    }
}
