using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public static class ArtefactContentRules
{
    public const int TitleMaxLength = 255;
    public const int DescriptionMaxLength = 500;
    public const int NotesMaxLength = 1000;

    public static bool IsValidTitle(string? title, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(title, TitleMaxLength, propertyName, out error);
    }

    public static bool IsValidDescription(string? description, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(description, DescriptionMaxLength, propertyName, out error);
    }

    public static bool IsValidNotes(string? notes, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotTooLong(notes, NotesMaxLength, propertyName, out error);
    }
}
