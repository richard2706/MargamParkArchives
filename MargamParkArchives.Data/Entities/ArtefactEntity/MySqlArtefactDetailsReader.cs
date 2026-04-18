using MargamParkArchives.Core.DataAccess.ArtefactEntity;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.ArtefactEntity;

/// <summary>
/// Methods for reading data from the artefact details database view.
/// </summary>
/// <remarks>
/// Useful for getting details about an artefact for display purposes as only one query is executed to get information
/// about an artefact from multiple tables.
/// </remarks>
/// <param name="dataAccess"></param>
public class MySqlArtefactDetailsReader(IMySqlDataAccess dataAccess) : IArtefactDetailsReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetRandomArtefactsQuery = "select * from {0} order by rand() limit @Limit;";
    private const string GetOneArtefactQuery = "select * from {0} where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";

    /// <summary>
    /// Returns an array of the specified number of artefact details items chosen at random from the database.
    /// </summary>
    /// <param name="numArtefacts">Number of random artefacts to return.</param>
    /// <returns>An array of the specified number of artefact details items chosen at random from the database.</returns>
    /// <exception cref="ArgumentException">If the number of artefacts requested is less than 1.</exception>
    public async Task<ArtefactDetailsReadModel[]> GetRandomArtefactsAsync(int numArtefacts = 3)
    {
        if (numArtefacts <= 0)
        {
            throw new ArgumentException("Number of artefacts must be greater than 0.");
        }

        string sqlQuery = string.Format(GetRandomArtefactsQuery, ArtefactDetailsViewName);
        IEnumerable<ArtefactDetailsDto> artefacts = await _dataAccess.GetManyItemsAsync<ArtefactDetailsDto, dynamic>(
            sqlQuery, new { Limit = numArtefacts });
        return artefacts.Select(dto => dto.ToArtefactDetailsReadModel()).ToArray();
    }

    /// <summary>
    /// Gets one Artefact Details record from the database, or null if it doesn't exist.
    /// </summary>
    /// <param name="identiferGroupId">Id of the identifier group used to uniquely identify the artefact (jointly with the identifier number).</param>
    /// <param name="identifierNumber">Value used to uniquely identify the artefact (along with the identifier group id).</param>
    /// <returns>The artefact details record identified by the identifier group id and identifier number, or null if it doesn't exist.</returns>
    public async Task<ArtefactDetailsReadModel?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber)
    {
        if (string.IsNullOrEmpty(identiferGroupId))
        {
            throw new ArgumentException("Identifier group id cannot be empty.", nameof(identiferGroupId));
        }
        else if (identifierNumber < 0)
        {
            throw new ArgumentException("Identifier number cannot be less than 0.", nameof(identifierNumber));
        }

        string sqlQuery = string.Format(GetOneArtefactQuery, ArtefactDetailsViewName);
        ArtefactDetailsDto? artefact = await _dataAccess.GetOneItemAsync<ArtefactDetailsDto?, object>(sqlQuery,
            new { IdentifierGroupId = identiferGroupId, IdentifierNumber = identifierNumber });
        return artefact?.ToArtefactDetailsReadModel();
    }
}
