namespace MargamParkArchives.Core.Entities.SpecificLocationEntity;

public class SpecificLocation
{
    public int Id { get; }
    public string Summary { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public SpecificLocation(int id, string summary, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!SpecificLocationValidationRules.IsValidSummary(summary, nameof(summary), out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Summary = summary;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
