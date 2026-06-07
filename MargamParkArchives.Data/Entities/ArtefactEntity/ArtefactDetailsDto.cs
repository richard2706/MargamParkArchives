using MargamParkArchives.Core.Entities.ArtefactDetails;

namespace MargamParkArchives.Data.Entities.ArtefactEntity;

internal class ArtefactDetailsDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names

    internal required string identifier_group_id { get; init; }
    internal required int identifier_number { get; init; }
    internal string? identifier_key { get; init; }
    internal string? identifer_group_name { get; init; }
    internal string? file_path { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }
    internal string? parent_id { get; init; }
    internal string? notes { get; init; }
    internal string? title_en { get; init; }
    internal string? title_cy { get; init; }
    internal string? description_en { get; init; }
    internal string? description_cy { get; init; }
    internal string? tags_cy { get; init; }
    internal string? culture_tag_en { get; init; }
    internal string? location_coverage { get; init; }
    internal string? right_type_1 { get; init; }
    internal string? right_holder_1_en { get; init; }
    internal string? right_holder_1_cy { get; init; }
    internal bool? visual_artefact { get; init; }
    internal string? category_id { get; init; }
    internal string? category_name { get; init; }
    internal int? creator_id { get; init; }
    internal string? creator_name { get; init; }
    internal int? general_location_id { get; init; }
    internal string? general_location_name { get; init; }
    internal int? specific_location_id { get; init; }
    internal string? specific_location_summary { get; init; }
    internal int? period_id { get; init; }
    internal string? period_dates { get; init; }

#pragma warning restore IDE1006

    internal ArtefactDetails ToArtefactDetailsReadModel() => new(identifier_group_id, identifier_number,
        identifier_key, identifer_group_name, category_id, category_name, creator_id, creator_name, general_location_id,
        general_location_name, specific_location_id, specific_location_summary, period_id, period_dates, file_path, date_created,
        date_modified, parent_id, notes, title_en, title_cy, description_en, description_cy, tags_cy, culture_tag_en,
        location_coverage, right_type_1, right_holder_1_en, right_holder_1_cy, visual_artefact);
}
