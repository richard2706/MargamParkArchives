using MargamParkArchives.Data.Entities.ArtefactEntity;

namespace MargamParkArchives.Data.Services;

/// <summary>
/// 
/// </summary>
public interface IArtefactSearchService
{
    public Task<IEnumerable<ArtefactRowQueryResultBase>> SearchArtefactsAsync(string searchTerm);
}
