namespace MargamParkArchivesData.Entities.ArtefactSubEntities;

public record TitleDescription
{
    public string? TitleEn { get; init; }
    public string? TitleCy { get; init; }
    public string? DescriptionEn { get; init; }
    public string? DescriptionCy { get; init; }
}
