using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.SpecificLocationEntity;

/// <summary>
/// Provides methods for creating, updating and deleting specific location records in the database.
/// </summary>
/// <param name="dataAccess">The data access service used to execute MySQL queries.</param>
public class MySqlSpecificLocationWriter(IMySqlDataAccess dataAccess) : ISpecificLocationWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertSpecificLocationQuery = "insert into {0} (summary) values (@Summary);";
    private const string UpdateSpecificLocationQuery = "update {0} set summary = @Summary where specific_location_id = @SpecificLocationId;";
    private const string DeleteSpecificLocationQuery = "delete from {0} where specific_location_id = @SpecificLocationId;";

    public async Task<int> CreateSpecificLocationAsync(SpecificLocationCreateDto specificLocation)
    {
        bool isSummaryValid = SpecificLocationValidationRules.IsValidSummary(
            specificLocation.Summary, nameof(specificLocation.Summary), out string summaryError);
        if (!isSummaryValid)
        {
            throw new ValidationException(summaryError, nameof(specificLocation.Summary));
        }

        string sqlQuery = string.Format(InsertSpecificLocationQuery, SpecificLocationTableName);
        return await _dataAccess.InsertAndReturnIdAsync(sqlQuery, specificLocation);
    }

    public async Task<bool> UpdateSpecificLocationAsync(SpecificLocationUpdateDto specificLocation)
    {
        bool isSummaryValid = SpecificLocationValidationRules.IsValidSummary(
            specificLocation.Summary, nameof(specificLocation.Summary), out string summaryError);
        if (!isSummaryValid)
        {
            throw new ValidationException(summaryError, nameof(specificLocation.Summary));
        }

        string sqlQuery = string.Format(UpdateSpecificLocationQuery, SpecificLocationTableName);
        return await _dataAccess.UpdateAsync<SpecificLocationUpdateDto>(sqlQuery, specificLocation);
    }

    public async Task<bool> DeleteSpecificLocationAsync(int specificLocationId)
    {
        if (specificLocationId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(specificLocationId), ValidationMessages.InvalidIntIdErrorMessage);
        }

        string sqlQuery = string.Format(DeleteSpecificLocationQuery, SpecificLocationTableName);
        int rowsDeleted = await _dataAccess.DeleteAsync(sqlQuery, new { SpecificLocationId = specificLocationId });
        return rowsDeleted > 0;
    }
}
