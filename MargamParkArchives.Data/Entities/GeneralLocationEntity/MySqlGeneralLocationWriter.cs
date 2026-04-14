using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

public class MySqlGeneralLocationWriter(IMySqlDataAccess dataAccess) : IGeneralLocationWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertGeneralLocationQuery = "insert into {0} (name) values (@Name);";
    private const string UpdateGeneralLocationQuery = "update {0} set name = @Name where general_location_id = @GeneralLocationId;";
    private const string DeleteGeneralLocationQuery = "delete from {0} where general_location_id = @GeneralLocationId;";

    public async Task<int> CreateGeneralLocationAsync(GeneralLocationCreateDto generalLocation)
    {
        bool isNameValid = GeneralLocationValidationRules.IsValidName(generalLocation.Name, nameof(generalLocation.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(generalLocation.Name));
        }

        string sqlQuery = string.Format(InsertGeneralLocationQuery, DatabaseConstants.GeneralLocationTableName);
        return await _dataAccess.InsertAndReturnIdAsync(sqlQuery, generalLocation);
    }

    public async Task<bool> UpdateGeneralLocationAsync(GeneralLocationUpdateDto generalLocation)
    {
        bool isNameValid = GeneralLocationValidationRules.IsValidName(generalLocation.Name, nameof(generalLocation.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(generalLocation.Name));
        }

        string sqlQuery = string.Format(UpdateGeneralLocationQuery, DatabaseConstants.GeneralLocationTableName);
        return await _dataAccess.UpdateAsync<GeneralLocationUpdateDto>(sqlQuery, generalLocation);
    }

    public async Task<bool> DeleteGeneralLocationAsync(int generalLocationId)
    {
        if (generalLocationId < 0)
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(generalLocationId));
        }

        string sqlQuery = string.Format(DeleteGeneralLocationQuery, GeneralLocationTableName);
        int rowsDeleted = await _dataAccess.DeleteAsync(sqlQuery, new { GeneralLocationId = generalLocationId });
        return rowsDeleted > 0;
    }
}
