using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

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

    public async Task<int> CreateSpecificLocationAsync(SpecificLocationCreateDto specificLocation)
    {
        bool isSummaryValid = SpecificLocationValidationRules.IsValidSummary(specificLocation.Summary, nameof(specificLocation.Summary), out string summaryError);
        if (!isSummaryValid)
        {
            throw new ValidationException(summaryError, nameof(specificLocation.Summary));
        }

        string sqlQuery = string.Format(InsertSpecificLocationQuery, DatabaseConstants.SpecificLocationTableName);
        return await _dataAccess.InsertAndReturnIdAsync(sqlQuery, specificLocation);
    }

    public async Task<bool> UpdateSpecificLocationAsync(SpecificLocationUpdateDto specificLocation)
    {
        bool isSummaryValid = SpecificLocationValidationRules.IsValidSummary(specificLocation.Summary, nameof(specificLocation.Summary), out string summaryError);
        if (!isSummaryValid)
        {
            throw new ValidationException(summaryError, nameof(specificLocation.Summary));
        }

        string sqlQuery = string.Format(UpdateSpecificLocationQuery, DatabaseConstants.SpecificLocationTableName);
        return await _dataAccess.UpdateAsync<SpecificLocationUpdateDto>(sqlQuery, specificLocation);
    }
}
