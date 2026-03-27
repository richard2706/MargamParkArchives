using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactRightsInformationValidationRules
{
    public const int RightsInformationMaxLength = 255;

    /// <summary>
    /// Determines whether the string value provided is valid as rights information based on maximum length constraints.
    /// </summary>
    /// <remarks>Rights information properties are RightType1, RightHolder1En or RightHolder1Cy</remarks>
    /// <param name="information">The information string to validate.</param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is valid as rights information.</returns>
    public static bool IsValidRightsInformation(string? information, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(information, RightsInformationMaxLength, propertyName, out error);
    }
}
