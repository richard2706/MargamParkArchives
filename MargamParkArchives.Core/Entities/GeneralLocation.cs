using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities;

public class GeneralLocation
{
    private const int NameMaxLength = 255;

    public int Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public GeneralLocation(int id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (name.Length == 0)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueEmptyMessage, nameof(Name)));
        }
        else if (name.Length > NameMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(Name),
                NameMaxLength));
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
