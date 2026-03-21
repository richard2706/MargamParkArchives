using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Entities.PeriodEntity;

/// <summary>
/// Provides 
/// </summary>
public class MySqlPeriodWriter(IMySqlDataAccess dataAccess) : IPeriodWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;
    private const string InsertPeriodQuery = "insert into {0} (period_id, dates) values (@PeriodId, @Dates);";
    private const string CreatePeriodFailedMessage = "Failed to create the new category in the database.";
    private const string UpdatePeriodQuery = "update {0} set period_id = @NewPeriodId, dates = @Dates where period_id = @ExistingPeriodId;";

    public async Task<int> CreatePeriodAsync(PeriodCreateDto period)
    {
        bool datesIsValid = PeriodValidationRules.IsValidDates(period.Dates, nameof(period.Dates), out string datesError);
        if (!datesIsValid)
        {
            throw new ValidationException(datesError, nameof(datesError));
        }

        string sqlQuery = string.Format(InsertPeriodQuery, DatabaseConstants.PeriodTableName);
        bool success = await _dataAccess.InsertAsync<PeriodCreateDto>(sqlQuery, period);
        return success ? period.PeriodId : throw new DatabaseException(CreatePeriodFailedMessage);
    }

    public async Task<bool> UpdatePeriodAsync(PeriodUpdateDto period)
    {
        bool datesIsValid = PeriodValidationRules.IsValidDates(period.Dates, nameof(period.Dates), out string datesError);
        if (!datesIsValid)
        {
            throw new ValidationException(datesError, nameof(datesError));
        }

        string sqlQuery = string.Format(UpdatePeriodQuery, DatabaseConstants.PeriodTableName);
        return await _dataAccess.UpdateAsync<PeriodUpdateDto>(sqlQuery, period);
    }
}
