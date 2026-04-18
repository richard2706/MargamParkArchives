using MargamParkArchives.Core.Entities.PeriodEntity;

namespace MargamParkArchives.Data.Entities.PeriodEntity;

internal record PeriodDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names
    internal required int period_id { get; init; }
    internal required string dates { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal Period ToPeriod() => new(period_id, dates, date_created, date_modified);
}
