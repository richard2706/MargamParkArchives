namespace MargamParkArchives.Data.Entities.ArtefactEntity;

/// <summary>
/// Base class holding properties of an artefact returned by most search queries, typically for use in tables.
/// </summary>
/// <remarks>
/// Properties will be displayed in both Admin and Explorer app tables, however each app may have additional properties.
/// </remarks>
public class ArtefactRowQueryResultBase
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names
    public required string? identifier_key { get; init; }
    public required string? identifier_group_name { get; init; }
    public required string? file_path { get; init; }
    public required DateTime? date_created { get; init; }
    public required string? description_en { get; init; }
    public required bool? visual_artefact { get; init; }
    public required string? category_name { get; init; }
    public required string? creator_name { get; init; }
    public required string? period_dates { get; init; }

#pragma warning restore IDE1006
}
