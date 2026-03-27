using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactContentValidationRules
{
    public const int TitleMaxLength = 255;
    public const int DescriptionMaxLength = 500;
    public const int NotesMaxLength = 1000;

    /// <summary>
    /// Determines whether the string value provided is a valid title based on maximum length constraints.
    /// </summary>
    /// <remarks>Rights information properties are TitleEn, TitleCy</remarks>
    /// <param name="information">The title string to validate.</param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is a valid title.</returns>
    public static bool IsValidTitle(string? title, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(title, TitleMaxLength, propertyName, out error);
    }

    /// <summary>
    /// Determines whether the string value provided is a valid description based on maximum length constraints.
    /// </summary>
    /// <remarks>Rights information properties are DescriptionEn, DescriptionCy</remarks>
    /// <param name="information">The description string to validate.</param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is a valid description.</returns>
    public static bool IsValidDescription(string? description, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(description, DescriptionMaxLength, propertyName, out error);
    }

    /// <summary>
    /// Returns true if the string value provided is a valid note based on maximum length constraints.
    /// </summary>
    /// <param name="notes"></param>
    /// <param name="propertyName">The name of the property being validated. Used in error messages.</param>
    /// <param name="error">Error message if the validation fails.</param>
    /// <returns>True if the value is a valid notes.</returns>
    public static bool IsValidNotes(string? notes, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(notes, NotesMaxLength, propertyName, out error);
    }
}
