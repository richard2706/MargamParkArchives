using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.SpecificLocationEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlSpecificLocationReader(IMySqlDataAccess dataAccess) : ISpecificLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllSpecificLocationsQuery = "select * from {0};";
    private const string GetOneSpecificLocationQuery = "select * from {0} where specific_location_id = @SpecificLocationId;";

    /// <summary>
    /// Returns an array of all specific locations in the database. The array will be empty if the database contains no specific locations.
    /// </summary>
    /// <returns>An array of all specific locations in the database. The array will be empty if the database contains no specific locations.</returns>
    public async Task<SpecificLocation[]> GetAllSpecificLocationsAsync()
    {
        string sqlQuery = string.Format(GetAllSpecificLocationsQuery, SpecificLocationTableName);
        IEnumerable<SpecificLocationDto> specificLocationDtos = await _dataAccess.GetManyItemsAsync<SpecificLocationDto>(sqlQuery);
        return specificLocationDtos.Select(dto => dto.ToSpecificLocation()).ToArray();
    }

    /// <summary>
    /// Returns one specific location from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the specific location.</param>
    /// <returns>The specific location identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public async Task<SpecificLocation?> GetOneSpecificLocationAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 0.");
        }

        string sqlQuery = string.Format(GetOneSpecificLocationQuery, SpecificLocationTableName);
        SpecificLocationDto? specificLocationDto = await _dataAccess.GetOneItemAsync<SpecificLocationDto?, object>(sqlQuery, new { SpecificLocationId = id });
        return specificLocationDto?.ToSpecificLocation();
    }
}
