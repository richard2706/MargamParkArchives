namespace MargamParkArchives.Core.Entities;

public class Category
{
    private const int MaxIdLength = 2;
    private const int MaxNameLength = 50;

    private const string IdTooLongMessage = "Category IDs cannot be longer than {0} characters";
    private const string NameTooLongMessage = "Category name cannot be longer than {0} characters";

    public string Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Category(string id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (id.Length > MaxIdLength)
        {
            throw new ArgumentException(string.Format(IdTooLongMessage, IdTooLongMessage));
        }
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException(string.Format(NameTooLongMessage, MaxNameLength);
        }
        if (dateModified < dateCreated)
        {
            throw new ArgumentException("Date modified cannot be earlier than date created");
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
