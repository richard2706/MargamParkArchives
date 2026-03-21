namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public record PeriodCreateDto
{
    public required int PeriodId { get; set; }
    public required string Dates { get; init; }
}
