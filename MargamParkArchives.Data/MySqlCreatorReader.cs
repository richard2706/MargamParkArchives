using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CreatorEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlCreatorReader(IMySqlDataAccess dataAccess) : ICreatorReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllCreatorsQuery = "select * from {0};";
    private const string GetOneCreatorQuery = "select * from {0} where creator_id = @CreatorId;";

    /// <summary>
    /// Returns an array of all creators in the database.
    /// </summary>
    /// <returns>An array of all creators in the database.</returns>
    public async Task<Creator[]> GetAllCreatorsAsync()
    {
        string sqlQuery = string.Format(GetAllCreatorsQuery, CreatorTableName);
        IEnumerable<CreatorDto> creatorDtos = await _dataAccess.GetManyItemsAsync<CreatorDto>(sqlQuery);
        return creatorDtos.Select(dto => dto.ToCreator()).ToArray();
    }

    /// <summary>
    /// Returns one creator from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the creator.</param>
    /// <returns>The creator identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public async Task<Creator?> GetOneCreatorAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 0.");
        }

        string sqlQuery = string.Format(GetOneCreatorQuery, CreatorTableName);
        CreatorDto? creatorDto = await _dataAccess.GetOneItemAsync<CreatorDto?, object>(sqlQuery, new { CreatorId = id });
        return creatorDto?.ToCreator();
    }
}
