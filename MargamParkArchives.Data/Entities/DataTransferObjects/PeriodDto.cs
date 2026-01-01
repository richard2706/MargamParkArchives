namespace MargamParkArchives.Data.Entities.DataTransferObjects;

internal record PeriodDto
{
    internal required int PeriodId { get; init; }
    internal required string Dates { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
