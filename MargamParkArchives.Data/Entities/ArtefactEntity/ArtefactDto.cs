using MargamParkArchives.Core.Entities.ArtefactEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Data.Entities.ArtefactEntity;

internal record ArtefactDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names

    internal required string identifier_group_id { get; init; }
    internal required int identifier_number { get; init; }
    internal string? identifier_key { get; init; }
    internal string? file_path { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }
    internal string? parent_id { get; init; }
    internal string? notes { get; init; }
    internal string? title_en { get; init; }
    internal string? title_cy { get; init; }
    internal string? description_en { get; init; }
    internal string? description_cy { get; init; }
    internal string? category_id { get; init; }
    internal string? tags_cy { get; init; }
    internal string? culture_tag_en { get; init; }
    internal int? period_id { get; init; }
    internal int? creator_id { get; init; }
    internal string? location_coverage { get; init; }
    internal string? right_type_1 { get; init; }
    internal string? right_holder_1_en { get; init; }
    internal string? right_holder_1_cy { get; init; }
    internal bool? visual_artefact { get; init; }
    internal int? general_location_id { get; init; }
    internal int? specific_location_id { get; init; }

#pragma warning restore IDE1006

    internal Artefact ToArtefact(IdentifierGroup identifierGroup, Category? category, Period? period, Creator? creator,
        GeneralLocation? generalLocation, SpecificLocation? specificLocation)
    {
        ArtefactRightsInformation rightsInformation = new(right_type_1, right_holder_1_en, right_holder_1_cy);
        ArtefactContent content = new(title_en, title_cy, description_en, description_cy, notes);
        ArtefactClassification classification = new(parent_id, tags_cy, culture_tag_en, location_coverage, visual_artefact);

        return new(identifierGroup, identifier_number, identifier_key, category, period, creator, generalLocation,
            specificLocation, file_path, date_created, date_modified, rightsInformation, content, classification);
    }
}
