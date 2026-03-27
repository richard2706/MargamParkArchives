using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.PeriodEntity;

public class MySqlPeriodReader(IMySqlDataAccess dataAccess) : IPeriodReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllPeriodsQuery = "select * from {0};";
    private const string GetOnePeriodQuery = "select * from {0} where period_id = @PeriodId;";
    private const string CheckPeriodExistsQuery = "select exists(select 1 from {0} where period_id = @PeriodId);";

    public async Task<Period[]> GetAllPeriodsAsync()
    {
        string sqlQuery = string.Format(GetAllPeriodsQuery, PeriodTableName);
        IEnumerable<PeriodDto> periodDtos = await _dataAccess.GetManyItemsAsync<PeriodDto>(sqlQuery);
        return periodDtos.Select(dto => dto.ToPeriod()).ToArray();
    }

    public async Task<Period?> GetOnePeriodAsync(int id)
    {
        string sqlQuery = string.Format(GetOnePeriodQuery, PeriodTableName);
        PeriodDto? periodDto = await _dataAccess.GetOneItemAsync<PeriodDto?, object>(sqlQuery, new { PeriodId = id });
        return periodDto?.ToPeriod();
    }

    public async Task<bool> PeriodExists(int id)
    {
        // Note that the id is freely chosen by the user, so negative numbers are valid ids and we don't throw an
        // error for them.

        string sqlQuery = string.Format(CheckPeriodExistsQuery, DatabaseConstants.PeriodTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { PeriodId = id });
    }
}
