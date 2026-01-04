using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.PeriodEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlPeriodReader(IMySqlDataAccess dataAccess) : IPeriodReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllPeriodsQuery = "select * from {0};";

    /// <summary>
    /// Returns an array of all periods in the database. The array will be empty if the database contains no periods.
    /// </summary>
    /// <returns>An array of all periods in the database. The array will be empty if the database contains no periods.</returns>
    public async Task<Period[]> GetAllPeriodsAsync()
    {
        string sqlQuery = string.Format(GetAllPeriodsQuery, PeriodTableName);
        IEnumerable<PeriodDto> periodDtos = await _dataAccess.GetManyItemsAsync<PeriodDto>(sqlQuery);
        return periodDtos.Select(dto => dto.ToPeriod()).ToArray();
    }

    /// <summary>
    /// Returns one period from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the period.</param>
    /// <returns>The period identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public Task<Period?> GetOnePeriodAsync(int id)
    {
        throw new NotImplementedException();
    }
}
