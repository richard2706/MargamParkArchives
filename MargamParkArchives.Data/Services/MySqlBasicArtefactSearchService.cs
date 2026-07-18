using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.ArtefactEntity;

namespace MargamParkArchives.Data.Services;

/// <summary>
/// Search service for providing row query results with fields for display in non admin views
/// </summary>
/// <param name="dataAccess"></param>
public class MySqlBasicArtefactSearchService(IMySqlDataAccess dataAccess) :
    MySqlArtefactSearchServiceBase<ArtefactRowQueryResultBase>(dataAccess),
    IArtefactSearchService;
