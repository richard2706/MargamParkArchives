using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public class Artefact
{
    public const int MinIdentifierNumber = 1;
    public const string InvalidIdentifierNumberMessage = "Identifier number must be greater than or equal to {0}.";

    // Identifying attributes
    public IdentifierGroup IdentifierGroup { get; }
    public int IdentifierNumber { get; }
    public string IdentifierKey { get; }

    // Linked entities
    public Category? Category { get; }
    public Period? Period { get; }
    public Creator? Creator { get; }
    public GeneralLocation? GeneralLocation { get; }
    public SpecificLocation? SpecificLocation { get; }

    // Artefact details
    public string? FilePath { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }
    public ArtefactRightsInformation? RightsInformation { get; }
    public ArtefactContent? Content { get; }
    public ArtefactClassification? Classification { get; }

    public Artefact(IdentifierGroup identifierGroup, int identifierNumber, string? identifierKey = null,
        Category? category = null, Period? period = null, Creator? creator = null,
        GeneralLocation? generalLocation = null, SpecificLocation? specificLocation = null, string? filePath = null,
        DateTime? dateCreated = null, DateTime? dateModified = null, ArtefactRightsInformation? rightsInformation = null,
        ArtefactContent? content = null, ArtefactClassification? classification = null)
    {
        if (identifierNumber < MinIdentifierNumber)
        {
            string errorMessage = string.Format(InvalidIdentifierNumberMessage, MinIdentifierNumber);
            throw new ArgumentException(errorMessage, nameof(identifierNumber));
        }

        IdentifierGroup = identifierGroup;
        IdentifierNumber = identifierNumber;
        IdentifierKey = identifierKey ?? IdentifierKeyHelper.BuildIdentifierKey(identifierGroup.Id, identifierNumber);
        Category = category;
        Period = period;
        Creator = creator;
        GeneralLocation = generalLocation;
        SpecificLocation = specificLocation;
        FilePath = filePath;
        DateCreated = dateCreated;
        DateModified = dateModified;
        RightsInformation = rightsInformation;
        Content = content;
        Classification = classification;
    }
}
