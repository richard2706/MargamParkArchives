using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using MargamParkArchives.Data.Entities.CreatorEntity;
using MargamParkArchives.Data.Entities.IdentifierGroupEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlIdentifierGroupReader(IMySqlDataAccess dataAccess) : IIdentifierGroupReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllIdentifierGroupsQuery = "select * from {0};";
    private const string GetOneIdentifierGroupQuery = "select * from {0} where identifier_group_id = @IdentifierGroupId;";

    /// <summary>
    /// Returns an array of all identifier groups in the database. The array will be empty if the database contains no creators.
    /// </summary>
    /// <returns>An array of all identifier groups in the database. The array will be empty if the database contains no creators.</returns>
    public async Task<IdentifierGroup[]> GetAllIdentifierGroupsAsync()
    {
        string sqlQuery = string.Format(GetAllIdentifierGroupsQuery, IdentifierGroupTableName);
        IEnumerable<IdentifierGroupDto> identifierGroupDtos = await _dataAccess.GetManyItemsAsync<IdentifierGroupDto>(sqlQuery);
        return identifierGroupDtos.Select(dto => dto.ToIdentifierGroup()).ToArray();
    }

    /// <summary>
    /// Returns one identifier group from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the identifier group.</param>
    /// <returns>The identifier group identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is an empty string then it is invalid.</exception>
    public async Task<IdentifierGroup?> GetOneIdentifierGroupAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Id cannot be an empty string.", nameof(id));
        }

        string sqlQuery = string.Format(GetOneIdentifierGroupQuery, IdentifierGroupTableName);
        IdentifierGroupDto? identifierGroupDto = await _dataAccess.GetOneItemAsync<IdentifierGroupDto?, object>(sqlQuery, new { IdentifierGroupId = id });
        return identifierGroupDto?.ToIdentifierGroup();
    }
}
