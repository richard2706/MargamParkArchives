using MargamParkArchives.Core.DataAccess.CreatorEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Entities.CreatorEntity;

public class MySqlCreatorWriter(IMySqlDataAccess dataAccess) : ICreatorWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertCreatorQuery = "insert into {0} (creator_id, name) values (@Creator, @Name);";
    private const string UpdateCreatorQuery = "update {0} set name = @Name where creator_id = @CreatorId;";
    private const string DeleteCreatorQuery = "delete from {0} where creator_id = @CreatorId;";

    public async Task<int> CreateCreatorAsync(CreatorCreateDto creator)
    {
        bool isNameValid = CreatorValidationRules.IsValidName(creator.Name, nameof(creator.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(creator.Name));
        }

        string sqlQuery = string.Format(InsertCreatorQuery, DatabaseConstants.CreatorTableName);
        return await _dataAccess.InsertAndReturnIdAsync<CreatorCreateDto>(sqlQuery, creator);
    }

    public async Task<bool> UpdateCreatorAsync(CreatorUpdateDto creator)
    {
        bool isNameValid = CreatorValidationRules.IsValidName(creator.Name, nameof(creator.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(creator.Name));
        }

        string sqlQuery = string.Format(UpdateCreatorQuery, DatabaseConstants.CreatorTableName);
        return await _dataAccess.UpdateAsync<CreatorUpdateDto>(sqlQuery, creator);
    }

    public async Task<bool> DeleteCreatorAsync(int creatorId)
    {
        if (creatorId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(creatorId), ValidationMessages.InvalidIntIdErrorMessage);
        }

        string sqlQuery = string.Format(DeleteCreatorQuery, DatabaseConstants.CreatorTableName);
        int rowsDeleted = await _dataAccess.DeleteAsync(sqlQuery, new { CreatorId = creatorId });
        return rowsDeleted > 0;
    }
}
