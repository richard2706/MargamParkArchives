using MargamParkArchives.Core.Entities.PeriodEntity;

namespace MargamParkArchives.Data.Entities.PeriodEntity;

internal record PeriodDto
{
    internal required int PeriodId { get; init; }
    internal required string Dates { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }

    internal Period ToPeriod() => new(PeriodId, Dates, DateCreated, DateModified);
}
