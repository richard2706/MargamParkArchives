using MargamParkArchives.Core.Entities.GeneralLocationEntity;

namespace MargamParkArchives.Core.DataAccess.GeneralLocationEntity;

public interface IGeneralLocationReader
{
    public Task<GeneralLocation[]> GetAllGeneralLocationsAsync();
    public Task<GeneralLocation?> GetOneGeneralLocationAsync(int id);
}
