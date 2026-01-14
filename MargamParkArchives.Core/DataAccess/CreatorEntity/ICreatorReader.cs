using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Core.DataAccess.CreatorEntity;

public interface ICreatorReader
{
    public Task<Creator[]> GetAllCreatorsAsync();
    public Task<Creator?> GetOneCreatorAsync(int id);
}
