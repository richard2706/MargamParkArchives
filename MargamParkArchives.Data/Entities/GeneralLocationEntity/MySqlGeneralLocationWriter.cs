using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

public class MySqlGeneralLocationWriter(IMySqlDataAccess dataAccess) : IGeneralLocationWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertCreatorQuery = "insert into {0} (name) values (@Name);";
    private const string UpdateCreatorQuery = "update {0} set name = @Name where general_location_id = @GeneralLocationId;";

    public async Task<int> CreateGeneralLocationAsync(GeneralLocationCreateDto generalLocation)
    {
        bool isNameValid = GeneralLocationValidationRules.IsValidName(generalLocation.Name, nameof(generalLocation.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(generalLocation.Name));
        }

        string sqlQuery = string.Format(InsertCreatorQuery, DatabaseConstants.GeneralLocationTableName);
        return await _dataAccess.InsertAndReturnIdAsync(sqlQuery, generalLocation);
    }

    public async Task<bool> UpdateGeneralLocationAsync(GeneralLocationUpdateDto generalLocation)
    {
        bool isNameValid = GeneralLocationValidationRules.IsValidName(generalLocation.Name, nameof(generalLocation.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(generalLocation.Name));
        }

        string sqlQuery = string.Format(UpdateCreatorQuery, DatabaseConstants.GeneralLocationTableName);
        return await _dataAccess.UpdateAsync<GeneralLocationUpdateDto>(sqlQuery, generalLocation);
    }
}
