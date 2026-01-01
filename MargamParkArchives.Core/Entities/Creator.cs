namespace MargamParkArchives.Core.Entities;

public class Creator
{
    private const string NameEmptyMessage = "Name must be at least 1 character in length.";
    private const string DateModifiedBeforeCreatedMessage = "Date modified cannot be earlier than date created";

    public int Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Creator(int id, string name, DateTime? dateCreated, DateTime? dateModified)
    {
        if (name.Length == 0)
        {
            throw new ArgumentException(NameEmptyMessage);
        }
        if (dateModified < dateCreated)
        {
            throw new ArgumentException(DateModifiedBeforeCreatedMessage);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
