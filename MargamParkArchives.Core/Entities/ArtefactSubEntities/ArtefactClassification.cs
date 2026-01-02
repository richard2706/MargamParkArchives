using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.ArtefactSubEntities;

public class ArtefactClassification
{
    private const int ParentIdMaxLength = 50;
    private const int TagsCyMaxLength = 255;
    private const int CultureTagEnMaxLength = 255;
    private const int LocationCoverageMaxLength = 255;

    public string? ParentId { get; }
    public string? TagsCy { get; }
    public string? CultureTagEn { get; }
    public string? LocationCoverage { get; }
    public bool? VisualArtefact { get; }

    public ArtefactClassification(string? parentId = null, string? tagsCy = null, string? cultureTagEn = null,
        string? locationCoverage = null, bool? visualArtefact = null)
    {
        ParentId = parentId;
        TagsCy = tagsCy;
        CultureTagEn = cultureTagEn;
        LocationCoverage = locationCoverage;
        VisualArtefact = visualArtefact;
    }
}
