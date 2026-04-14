using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.PeriodEntity;

/// <summary>
/// Provides 
/// </summary>
public class MySqlPeriodWriter(IMySqlDataAccess dataAccess) : IPeriodWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertPeriodQuery = "insert into {0} (period_id, dates) values (@PeriodId, @Dates);";
    private const string UpdatePeriodQuery = "update {0} set period_id = @NewPeriodId, dates = @Dates where period_id = @ExistingPeriodId;";
    private const string DeletePeriodQuery = "delete from {0} where period_id = @PeriodId;";

    private const string CreatePeriodFailedMessage = "Failed to create the new category in the database.";

    public async Task<int> CreatePeriodAsync(PeriodCreateDto period)
    {
        bool datesIsValid = PeriodValidationRules.IsValidDates(period.Dates, nameof(period.Dates), out string datesError);
        if (!datesIsValid)
        {
            throw new ValidationException(datesError, nameof(period.Dates));
        }

        string sqlQuery = string.Format(InsertPeriodQuery, PeriodTableName);
        bool success = await _dataAccess.InsertAsync<PeriodCreateDto>(sqlQuery, period);
        return success ? period.PeriodId : throw new DatabaseException(CreatePeriodFailedMessage);
    }

    public async Task<bool> UpdatePeriodAsync(PeriodUpdateDto period)
    {
        bool datesIsValid = PeriodValidationRules.IsValidDates(period.Dates, nameof(period.Dates), out string datesError);
        if (!datesIsValid)
        {
            throw new ValidationException(datesError, nameof(period.Dates));
        }

        string sqlQuery = string.Format(UpdatePeriodQuery, PeriodTableName);
        return await _dataAccess.UpdateAsync<PeriodUpdateDto>(sqlQuery, period);
    }

    public async Task<bool> DeletePeriodAsync(int periodId)
    {
        if (periodId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(periodId), InvalidIdErrorMessage);
        }

        string sqlQuery = string.Format(DeletePeriodQuery, PeriodTableName);
        int rowsDeleted = await _dataAccess.DeleteAsync(sqlQuery, new { PeriodId = periodId });
        return rowsDeleted > 0;
    }
}
