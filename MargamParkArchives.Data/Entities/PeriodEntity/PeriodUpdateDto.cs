namespace MargamParkArchives.Data.Entities.PeriodEntity;

internal record PeriodUpdateDto
{
    internal required int PeriodId { get; init; }
    internal required string Dates { get; init; }
}
