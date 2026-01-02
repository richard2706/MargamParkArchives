using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities;

public class Period
{
    private const int DatesMaxLength = 50;

    public int Id { get; }
    public string Dates { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Period(int id, string dates, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (dates.Length == 0)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueEmptyMessage, nameof(Dates)));
        }
        else if (dates.Length > DatesMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(Dates),
                DatesMaxLength));
        }

        Id = id;
        Dates = dates;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
