using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Core.DataAccess.SpecificLocationEntity;

public interface ISpecificLocationReader
{
    public Task<SpecificLocation[]> GetAllSpecificLocationsAsync();
    public Task<SpecificLocation?> GetOneSpecificLocationAsync(int id);
}
