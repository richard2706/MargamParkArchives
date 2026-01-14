using MargamParkArchives.Core.Entities.PeriodEntity;

namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public interface IPeriodReader
{
    public Task<Period[]> GetAllPeriodsAsync();
    public Task<Period?> GetOnePeriodAsync(int id);
}
