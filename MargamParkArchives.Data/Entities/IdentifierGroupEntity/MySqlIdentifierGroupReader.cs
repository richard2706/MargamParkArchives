using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.IdentifierGroupEntity;

public class MySqlIdentifierGroupReader(IMySqlDataAccess dataAccess) : IIdentifierGroupReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllIdentifierGroupsQuery = "select * from {0};";
    private const string GetOneIdentifierGroupQuery = "select * from {0} where identifier_group_id = @IdentifierGroupId;";
    private const string CheckIdentifierGroupExistsQuery = "select exists(select 1 from {0} where identifier_group_id = @IdentifierGroupId);";
    private const string InvalidIdErrorMessage = "Id cannot be an empty string.";

    public async Task<IdentifierGroup[]> GetAllIdentifierGroupsAsync()
    {
        string sqlQuery = string.Format(GetAllIdentifierGroupsQuery, IdentifierGroupTableName);
        IEnumerable<IdentifierGroupDto> identifierGroupDtos = await _dataAccess.GetManyItemsAsync<IdentifierGroupDto>(sqlQuery);
        return identifierGroupDtos.Select(dto => dto.ToIdentifierGroup()).ToArray();
    }

    public async Task<IdentifierGroup?> GetOneIdentifierGroupAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(GetOneIdentifierGroupQuery, IdentifierGroupTableName);
        IdentifierGroupDto? identifierGroupDto = await _dataAccess.GetOneItemAsync<IdentifierGroupDto?, object>(sqlQuery, new { IdentifierGroupId = id });
        return identifierGroupDto?.ToIdentifierGroup();
    }

    public async Task<bool> IdentifierGroupExists(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(CheckIdentifierGroupExistsQuery, DatabaseConstants.IdentifierGroupTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { IdentifierGroupId = id });
    }
}
