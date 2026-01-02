namespace MargamParkArchives.Core.Entities.PeriodEntity;

public class Period
{
    public int Id { get; }
    public string Dates { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Period(int id, string dates, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!PeriodRules.IsValidDates(dates, nameof(dates), out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Dates = dates;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
