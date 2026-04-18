using MargamParkArchives.Core.DataAccess.ArtefactEntity;
using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.DataAccess.CreatorEntity;
using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.ArtefactEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.ArtefactEntity;
using System.Text;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities;

public class MySqlArtefactReader(IMySqlDataAccess dataAccess, IIdentifierGroupReader identifierGroupReader,
    ICategoryReader categoryReader, IPeriodReader periodReader, ICreatorReader creatorReader,
    IGeneralLocationReader generalLocationReader, ISpecificLocationReader specificLocationReader) : IArtefactReader
{
    private const string GetOneArtefactQuery = "select * from {0} where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";

    private const string IdentifierGroupNotFoundMessage = "Identifier group {0} for the artefact {1} could not be found in the database.";

    private const string CheckArtefactExistsByIdentifierIdAndNumberQueryTemplate = "select exists(select 1 from {0} where " +
        "identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber);";

    private const string CheckArtefactExistsByIdentifierKeyQueryTemplate = "select exists(select 1 from {0} where " +
        "identifier_key = @IdentifierKey);";

    private static string CheckArtefactExistsByIdentifierIdAndNumberQuery =>
        string.Format(CheckArtefactExistsByIdentifierIdAndNumberQueryTemplate, DatabaseConstants.ArtefactTableName);

    private static string CheckArtefactExistsByIdentifierKeyQuery =>
        string.Format(CheckArtefactExistsByIdentifierKeyQueryTemplate, DatabaseConstants.ArtefactTableName);

    private readonly IMySqlDataAccess _dataAccess = dataAccess;
    private readonly IIdentifierGroupReader _identifierGroupReader = identifierGroupReader;
    private readonly ICategoryReader _categoryReader = categoryReader;
    private readonly IPeriodReader _periodReader = periodReader;
    private readonly ICreatorReader _creatorReader = creatorReader;
    private readonly IGeneralLocationReader _generalLocationReader = generalLocationReader;
    private readonly ISpecificLocationReader _specificLocationReader = specificLocationReader;

    /// <summary>
    /// Gets one Artefact from the database (including all linked entities), or null if it doesn't exist.
    /// </summary>
    /// <param name="identiferGroupId">Id of the identifier group used to uniquely identify the artefact (jointly with
    /// the identifier number).</param>
    /// <param name="identifierNumber">Value used to uniquely identify the artefact (jointly with the identifier group
    /// id).</param>
    /// <returns>The artefact identified by the identifier group id and identifier number, or null if it doesn't
    /// exist.</returns>
    public async Task<Artefact?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber)
    {
        if (string.IsNullOrWhiteSpace(identiferGroupId))
        {
            throw new ArgumentException(ValidationMessages.EmptyStringIdErrorMessage, nameof(identiferGroupId));
        }
        else if (identifierNumber < 0)
        {
            throw new ArgumentException(ValidationMessages.InvalidIntIdErrorMessage, nameof(identifierNumber));
        }

        // Get artefact
        string sqlQuery = string.Format(GetOneArtefactQuery, ArtefactTableName);
        ArtefactDto? artefact = await _dataAccess.GetOneItemAsync<ArtefactDto?, object>(
            sqlQuery,
            new { IdentifierGroupId = identiferGroupId, IdentifierNumber = identifierNumber });
        if (artefact == null)
        {
            return null;
        }

        // Get linked entities
        IdentifierGroup? identifierGroup =
            await _identifierGroupReader.GetOneIdentifierGroupAsync(artefact.identifier_group_id);
        if (identifierGroup == null)
        {
            string identiferKey = artefact.identifier_key
                ?? IdentifierKeyHelper.BuildIdentifierKey(artefact.identifier_group_id, identifierNumber);
            string message = string.Format(IdentifierGroupNotFoundMessage, artefact.identifier_group_id, identiferKey);
            throw new DataIntegrityException(message);
        }
        Category? category = artefact.category_id is string categoryId ?
            await _categoryReader.GetOneCategoryAsync(categoryId) : null;
        Period? period = artefact.period_id is int id ?
            await _periodReader.GetOnePeriodAsync(id) : null;
        Creator? creator = artefact.creator_id is int creatorId ?
            await _creatorReader.GetOneCreatorAsync(creatorId) : null;
        GeneralLocation? generalLocation = artefact.general_location_id is int generalLocationId ?
            await _generalLocationReader.GetOneGeneralLocationAsync(generalLocationId) : null;
        SpecificLocation? specificLocation = artefact.specific_location_id is int specificLocationId ?
            await _specificLocationReader.GetOneSpecificLocationAsync(specificLocationId) : null;

        return artefact.ToArtefact(identifierGroup, category, period, creator, generalLocation, specificLocation);
    }

    public async Task<bool> ArtefactExistsAsync(string identifierKey)
    {
        if (string.IsNullOrWhiteSpace(identifierKey))
        {
            throw new ArgumentException(ValidationMessages.EmptyStringIdErrorMessage, nameof(identifierKey));
        }

        return await _dataAccess.ExistsAsync<object>(CheckArtefactExistsByIdentifierKeyQuery, new { IdentifierKey = identifierKey });
    }

    public async Task<bool> ArtefactExistsAsync(string identifierGroupId, int identifierNumber, StringBuilder? errorList = null)
    {
        if (string.IsNullOrWhiteSpace(identifierGroupId))
        {
            if (errorList != null)
            {
                errorList.AppendLine(ValidationMessages.EmptyStringIdErrorMessage);
            }
            else
            {
                throw new ArgumentNullException(nameof(identifierGroupId));
            }
        }
        if (identifierNumber < Artefact.MinIdentifierNumber)
        {
            if (errorList != null)
            {
                errorList.AppendLine(string.Format(Artefact.InvalidIdentifierNumberMessage, Artefact.MinIdentifierNumber));
            }
            else
            {
                throw new ArgumentNullException(nameof(identifierNumber));
            }
        }

        return await _dataAccess.ExistsAsync<object>(CheckArtefactExistsByIdentifierIdAndNumberQuery,
            new { IdentifierGroupId = identifierGroupId, IdentifierNumber = identifierNumber });
    }
}
