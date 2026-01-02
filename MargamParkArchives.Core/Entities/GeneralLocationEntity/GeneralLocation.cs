namespace MargamParkArchives.Core.Entities.GeneralLocationEntity;

public class GeneralLocation
{
    public int Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public GeneralLocation(int id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!GeneralLocationRules.IsValidName(name, out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
