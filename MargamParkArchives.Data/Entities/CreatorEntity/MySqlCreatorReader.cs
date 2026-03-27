using MargamParkArchives.Core.DataAccess.CreatorEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.CreatorEntity;

public class MySqlCreatorReader(IMySqlDataAccess dataAccess) : ICreatorReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllCreatorsQuery = "select * from {0};";
    private const string GetOneCreatorQuery = "select * from {0} where creator_id = @CreatorId;";
    private const string CheckCreatorExistsQuery = "select exists(select 1 from {0} where creator_id = @CreatorId);";
    private const string InvalidIdErrorMessage = "Id cannot be less than 0.";

    public async Task<Creator[]> GetAllCreatorsAsync()
    {
        string sqlQuery = string.Format(GetAllCreatorsQuery, CreatorTableName);
        IEnumerable<CreatorDto> creatorDtos = await _dataAccess.GetManyItemsAsync<CreatorDto>(sqlQuery);
        return creatorDtos.Select(dto => dto.ToCreator()).ToArray();
    }

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

    public async Task<bool> CreatorExists(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(CheckCreatorExistsQuery, DatabaseConstants.CreatorTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { CreatorId = id });
    }
}
