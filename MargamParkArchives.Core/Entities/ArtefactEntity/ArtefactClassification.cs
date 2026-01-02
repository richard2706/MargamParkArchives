using MargamParkArchives.Core.Entities.Validation;

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
        if (parentId != null && parentId.Length > ParentIdMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(ParentId), ParentIdMaxLength));
        }
        if (tagsCy != null && tagsCy.Length > TagsCyMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(TagsCy), TagsCyMaxLength));
        }
        if (cultureTagEn != null && cultureTagEn.Length > CultureTagEnMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(CultureTagEn), CultureTagEnMaxLength));
        }
        if (locationCoverage != null && locationCoverage.Length > LocationCoverageMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(LocationCoverage), LocationCoverageMaxLength));
        }

        ParentId = parentId;
        TagsCy = tagsCy;
        CultureTagEn = cultureTagEn;
        LocationCoverage = locationCoverage;
        VisualArtefact = visualArtefact;
    }
}
