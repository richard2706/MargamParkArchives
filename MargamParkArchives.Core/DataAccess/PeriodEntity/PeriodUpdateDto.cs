namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public record PeriodUpdateDto
{
    public required int ExistingPeriodId { get; init; }
    public required int NewPeriodId { get; init; }
    public required string Dates { get; init; }
}
