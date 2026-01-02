namespace MargamParkArchives.Core.Entities.IdentifierGroupEntity;

public class IdentifierGroup
{
    public string Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public IdentifierGroup(string id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!IdentifierGroupRules.IsValidId(id, nameof(id), out string error))
        {
            throw new ArgumentException(error);
        }
        else if (!IdentifierGroupRules.IsValidName(name, nameof(id), out error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
