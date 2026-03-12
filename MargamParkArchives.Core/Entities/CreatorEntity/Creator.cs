namespace MargamParkArchives.Core.Entities.CreatorEntity;

public class Creator
{
    public int Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Creator(int id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!CreatorValidationRules.IsValidName(name, nameof(name), out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
