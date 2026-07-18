using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.ArtefactEntity;

namespace MargamParkArchives.Data.Services;

/// <summary>
/// Base class for artefact search methods allowing the caller to search for artefacts based on a search term
/// </summary>
/// <typeparam name="TQueryResult">The type of the query result that the search methods will return</typeparam>
/// <param name="dataAccess">The data access instance for reading data</param>
public abstract class MySqlArtefactSearchServiceBase<TQueryResult>(IMySqlDataAccess dataAccess)
    where TQueryResult : ArtefactRowQueryResultBase
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;
    private const string SearchTermEmptyMessage = "Search term cannot be null or empty.";

    protected virtual string SearchQuerySelectClause => "select " +
        "artefact.identifier_key, identifier_group.name, artefact.file_path, artefact.date_created, artefact.description_en, " +
        "artefact.visual_artefact, category.name, creator.name, period.dates";

    protected virtual string SearchQueryFromClause => "from artefact " +
        "inner join identifier_group on artefact.identifier_group_id = identifier_group.identifier_group_id" +
        "inner join category on artefact.category_id = category.category_id " +
        "inner join creator on artefact.creator_id = creator.creator_id " +
        "inner join period on artefact.period_id = period.period_id";

    protected virtual string SearchQueryWhereClause => "where " +
        "artefact.identifier_key like concat('%', @SearchTerm, '%') " +
        "or identifier_group.name like concat('%', @SearchTerm, '%') " +
        "or artefact.file_path like concat('%', @SearchTerm, '%') " +
        //"or artefact.date_created like concat('%', @SearchTerm, '%') " +
        "or artefact.description_en like concat('%', @SearchTerm, '%') " +
        "or category.name like concat('%', @SearchTerm, '%') " +
        "or creator.name like concat('%', @SearchTerm, '%') " +
        "or period.dates like concat('%', @SearchTerm, '%')";

    /// <summary>
    /// Complete search query that accepts the search term as a parameter. Built from the select, from and where clauses defined in the derived class.
    /// </summary>
    protected string SearchQuery => string.Format("{0} {1} {2}", this.SearchQuerySelectClause, this.SearchQueryFromClause,
        this.SearchQueryWhereClause);

    /// <summary>
    /// Returns artefacts that match the search term in any of the searched fields.
    /// </summary>
    /// <param name="searchTerm">String to search for in all fields (except dates)</param>
    /// <returns>Collection of artefact query results matching the search term</returns>
    /// <exception cref="ArgumentNullException">If the search term is null or empty</exception>
    public async Task<IEnumerable<ArtefactRowQueryResultBase>> SearchArtefactsAsync(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            throw new ArgumentNullException(nameof(searchTerm), SearchTermEmptyMessage);
        }

        IEnumerable<TQueryResult> result = await _dataAccess.GetManyItemsAsync<TQueryResult, dynamic>(this.SearchQuery,
            new { SearchTerm = searchTerm });
        return result;
    }
}
