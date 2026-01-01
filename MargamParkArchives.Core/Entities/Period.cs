namespace MargamParkArchives.Core.Entities;

public class Period
{
    public int Id { get; }
    public string Dates { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Period(int id, string dates, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        Id = id;
        Dates = dates;

        if (dateModified < dateCreated)
        {
            throw new ArgumentException("Date modified cannot be earlier than date created");
        }
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
