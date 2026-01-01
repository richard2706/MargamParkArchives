namespace MargamParkArchives.Core.Entities;

public class Creator
{
    private const string NameEmptyMessage = "Name must be at least 1 character in length.";

    public int Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Creator(int id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (name.Length == 0)
        {
            throw new ArgumentException(NameEmptyMessage);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
