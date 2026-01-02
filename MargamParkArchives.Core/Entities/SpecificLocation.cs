using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities;

public class SpecificLocation
{
    private const int SummaryMaxLength = 255;

    public int Id { get; }
    public string Summary { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public SpecificLocation(int id, string summary, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (summary.Length == 0)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueEmptyMessage, nameof(Summary));
        }
        else if (summary.Length > SummaryMaxLength)
        {
            throw new ArgumentException(string.Format(ValidationMessages.ValueTooLongMessage, nameof(Summary),
                SummaryMaxLength));
        }

        Id = id;
        Summary = summary;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
