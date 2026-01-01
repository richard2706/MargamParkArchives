namespace MargamParkArchives.Data.Entities.Period;

internal record PeriodUpdateDto
{
    internal required int PeriodId { get; init; }
    internal required string Dates { get; init; }
}
