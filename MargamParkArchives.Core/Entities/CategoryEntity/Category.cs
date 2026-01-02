namespace MargamParkArchives.Core.Entities.CategoryEntity;

public class Category
{
    public string Id { get; }
    public string Name { get; }
    public DateTime? DateCreated { get; }
    public DateTime? DateModified { get; }

    public Category(string id, string name, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        if (!CategoryRules.IsValidId(id, out string error))
        {
            throw new ArgumentException(error);
        }

        Id = id;
        Name = name;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
}
