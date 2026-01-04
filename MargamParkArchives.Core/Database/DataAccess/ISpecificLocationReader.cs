using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface ISpecificLocationReader
{
    public Task<SpecificLocation[]> GetAllSpecificLocationsAsync();
    public Task<SpecificLocation?> GetOneSpecificLocationAsync(int id);
}
