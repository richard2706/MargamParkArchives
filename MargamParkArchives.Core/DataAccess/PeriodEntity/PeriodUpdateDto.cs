namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public record PeriodUpdateDto
{
    public required int PeriodId { get; init; }
    public required string Dates { get; init; }
}
