using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities;

public class IdentifierGroup
{
    public string Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public IdentifierGroup(string id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!IdentifierGroupRules.IsValidId(id, out string error))
        {
            throw new ArgumentException(error);
        }
        else if (!IdentifierGroupRules.IsValidName(name, out error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
