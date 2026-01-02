namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public class ArtefactContent
{
    public string? TitleEn { get; }
    public string? TitleCy { get; }
    public string? DescriptionEn { get; }
    public string? DescriptionCy { get; }
    public string? Notes { get; }

    public ArtefactContent(string? titleEn = null, string? titleCy = null, string? descriptionEn = null, string? descriptionCy = null, string? notes = null)
    {
        if (!ArtefactContentRules.IsValidTitle(titleEn, nameof(titleEn), out string error))
        {
            throw new ArgumentException(error, nameof(titleEn));
        }
        else if (!ArtefactContentRules.IsValidTitle(titleCy, nameof(titleCy), out error))
        {
            throw new ArgumentException(error, nameof(titleCy));
        }
        else if (!ArtefactContentRules.IsValidDescription(descriptionEn, nameof(descriptionEn), out error))
        {
            throw new ArgumentException(error, nameof(descriptionEn));
        }
        else if (!ArtefactContentRules.IsValidDescription(descriptionCy, nameof(descriptionCy), out error))
        {
            throw new ArgumentException(error, nameof(descriptionCy));
        }
        else if (!ArtefactContentRules.IsValidNotes(notes, nameof(notes), out error))
        {
            throw new ArgumentException(error, nameof(notes));
        }

        TitleEn = titleEn;
        TitleCy = titleCy;
        DescriptionEn = descriptionEn;
        DescriptionCy = descriptionCy;
        Notes = notes;
    }
}
