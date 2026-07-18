using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.ArtefactEntity;

namespace MargamParkArchives.Data.Services;

/// <summary>
/// Search service for providing row query results with fields for display in admin views
/// </summary>
/// <param name="dataAccess"></param>
public class MySqlAdminArtefactSearchService(IMySqlDataAccess dataAccess) :
    MySqlArtefactSearchServiceBase<AdminArtefactRowQueryResult>(dataAccess),
    IArtefactSearchService
{
    protected override string SearchQuerySelectClause => base.SearchQuerySelectClause +
        ", artefact.date_modified, general_location.name, specific_location.summary";

    protected override string SearchQueryFromClause => base.SearchQueryFromClause +
        " inner join general_location on artefact.general_location_id = general_location.general_location_id " +
        "inner join specific_location on artefact.specific_location_id = specific_location.specific_location_id";

    protected override string SearchQueryWhereClause => base.SearchQueryWhereClause +
        //" or artefact.date_modified like concat('%', @SearchTerm, '%') " +
        " or general_location.name like concat('%', @SearchTerm, '%') " +
        "or specific_location.summary like concat('%', @SearchTerm, '%')";
}
