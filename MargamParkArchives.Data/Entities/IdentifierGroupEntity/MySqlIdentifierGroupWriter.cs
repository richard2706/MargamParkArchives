using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Entities.IdentifierGroupEntity;

public class MySqlIdentifierGroupWriter(IMySqlDataAccess dataAccess) : IIdentifierGroupWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertIdentifierGroupQuery = "insert into {0} (identifier_group_id, name) values (@IdentifierGroupId, @Name);";
    private const string CreateIdentifierGroupFailedMessage = "Failed to create the new identifier group in the database.";
    private const string UpdateIdentifierGroupQuery = "update {0} set identifier_group_id = @NewIdentifierGroupId, name = @Name where identifier_group_id = @ExistingIdentifierGroupId;";

    public async Task<string> CreateIdentifierGroupAsync(IdentifierGroupCreateDto identifierGroup)
    {
        bool idIsValid = IdentifierGroupValidationRules.IsValidIdentifierGroupId(
            identifierGroup.IdentifierGroupId, nameof(identifierGroup.IdentifierGroupId), out string idError);
        if (!idIsValid)
        {
            throw new ValidationException(idError, nameof(identifierGroup.IdentifierGroupId));
        }

        bool nameIsValid = IdentifierGroupValidationRules.IsValidName(identifierGroup.Name, nameof(identifierGroup.Name), out string nameError);
        if (!nameIsValid)
        {
            throw new ValidationException(nameError, nameof(identifierGroup.Name));
        }

        string sqlQuery = string.Format(InsertIdentifierGroupQuery, DatabaseConstants.IdentifierGroupTableName);
        bool success = await _dataAccess.InsertAsync(sqlQuery, identifierGroup);
        return success ? identifierGroup.IdentifierGroupId : throw new DatabaseException(CreateIdentifierGroupFailedMessage);
    }

    public async Task<bool> UpdateIdentifierGroupAsync(IdentifierGroupUpdateDto identifierGroup)
    {
        bool idIsValid = IdentifierGroupValidationRules.IsValidIdentifierGroupId(
            identifierGroup.NewIdentifierGroupId, nameof(identifierGroup.NewIdentifierGroupId), out string idError);
        if (!idIsValid)
        {
            throw new ValidationException(idError, nameof(identifierGroup.NewIdentifierGroupId));
        }

        bool nameIsValid = IdentifierGroupValidationRules.IsValidName(identifierGroup.Name, nameof(identifierGroup.Name), out string nameError);
        if (!nameIsValid)
        {
            throw new ValidationException(nameError, nameof(identifierGroup.Name));
        }

        string sqlQuery = string.Format(UpdateIdentifierGroupQuery, DatabaseConstants.IdentifierGroupTableName);
        return await _dataAccess.UpdateAsync<IdentifierGroupUpdateDto>(sqlQuery, identifierGroup);
    }
}
