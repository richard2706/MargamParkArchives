using MargamParkArchives.Core.Entities.IdentifierGroupEntity;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface IIdentifierGroupReader
{
    public Task<IdentifierGroup[]> GetAllIdentifierGroupsAsync();
    public Task<IdentifierGroup?> GetOneIdentifierGroupAsync(string id);
}
