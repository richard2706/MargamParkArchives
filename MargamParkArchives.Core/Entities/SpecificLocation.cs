namespace MargamParkArchives.Core.Entities;

public class SpecificLocation
{
    public int Id { get; }
    public int Summary { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public SpecificLocation(int id, int summary, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        Id = id;
        Summary = summary;

        if (dateModified < dateCreated)
        {
            throw new ArgumentException("Date modified cannot be earlier than date created");
        }
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
