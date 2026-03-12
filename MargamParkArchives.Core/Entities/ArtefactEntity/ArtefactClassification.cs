namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public class ArtefactClassification
{
    public string? ParentId { get; }
    public string? TagsCy { get; }
    public string? CultureTagEn { get; }
    public string? LocationCoverage { get; }
    public bool? VisualArtefact { get; }

    public ArtefactClassification(string? parentId = null, string? tagsCy = null, string? cultureTagEn = null,
        string? locationCoverage = null, bool? visualArtefact = null)
    {
        if (!ArtefactClassificationValidationRules.IsValidParentId(parentId, nameof(parentId), out string error))
        {
            throw new ArgumentException(error, nameof(parentId));
        }
        else if (!ArtefactClassificationValidationRules.IsValidClassificaionText(tagsCy, nameof(tagsCy), out error))
        {
            throw new ArgumentException(error, nameof(tagsCy));
        }
        else if (!ArtefactClassificationValidationRules.IsValidClassificaionText(cultureTagEn, nameof(cultureTagEn), out error))
        {
            throw new ArgumentException(error, nameof(cultureTagEn));
        }
        else if (!ArtefactClassificationValidationRules.IsValidClassificaionText(locationCoverage, nameof(locationCoverage), out error))
        {
            throw new ArgumentException(error, nameof(locationCoverage));
        }

        ParentId = parentId;
        TagsCy = tagsCy;
        CultureTagEn = cultureTagEn;
        LocationCoverage = locationCoverage;
        VisualArtefact = visualArtefact;
    }
}
