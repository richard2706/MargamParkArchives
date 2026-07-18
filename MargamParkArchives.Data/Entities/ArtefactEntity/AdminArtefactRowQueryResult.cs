namespace MargamParkArchives.Data.Entities.ArtefactEntity;

/// <summary>
/// Holds the properties of an artefact to be displayed in Admin Console tables.
/// </summary>
public class AdminArtefactRowQueryResult : ArtefactRowQueryResultBase
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names
    public required DateTime? date_modified { get; init; }
    public required string? general_location_name { get; init; }
    public required string? specific_location_summary { get; init; }
#pragma warning restore IDE1006
}
