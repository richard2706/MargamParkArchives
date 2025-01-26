namespace MargamParkArchives.Entities.ArtefactSubEntities
{
    internal record TitleDescription
    {
        internal string? TitleEn { get; init; }
        internal string? TitleCy { get; init; }
        internal string? DescriptionEn { get; init; }
        internal string? DescriptionCy { get; init; }
    }
}
