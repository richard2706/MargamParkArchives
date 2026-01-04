using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface ICreatorReader
{
    public Task<Creator[]> GetAllCreatorsAsync();
    public Task<Creator?> GetOneCreatorAsync(int id);
}
