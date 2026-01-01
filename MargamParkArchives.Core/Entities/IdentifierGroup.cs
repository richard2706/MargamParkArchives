namespace MargamParkArchives.Core.Entities;

public class IdentifierGroup
{
    private const int IdMaxLength = 3;
    private const int NameMaxLength = 255;

    private const string ValueEmptyMessage = "{0} must contain at least 1 character.";
    private const string ValueTooLongMessage = "{0} must not be longer than {1} characters.";

    public string Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public IdentifierGroup(string id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (id.Length == 0)
        {
            throw new ArgumentException(string.Format(ValueEmptyMessage, nameof(Id)));
        }
        else if (id.Length > IdMaxLength)
        {
            throw new ArgumentException(string.Format(ValueTooLongMessage, nameof(Id), IdMaxLength));
        }
        if (name.Length == 0)
        {
            throw new ArgumentException(string.Format(ValueEmptyMessage, nameof(Name)));
        }
        else if (name.Length > NameMaxLength)
        {
            throw new ArgumentException(string.Format(ValueTooLongMessage, nameof(Name), NameMaxLength));
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
