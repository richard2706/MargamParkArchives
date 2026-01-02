using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities;

public class SpecificLocation
{
    public int Id { get; }
    public string Summary { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public SpecificLocation(int id, string summary, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!SpecificLocationRules.IsValidSummary(summary, out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Summary = summary;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
