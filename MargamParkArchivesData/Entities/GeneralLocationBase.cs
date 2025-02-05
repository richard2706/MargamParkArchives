namespace MargamParkArchivesData.Entities
{
    /// <summary>
    /// Represents a general location entity from the database, exluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Name">Name of the general location.</param>
    public record GeneralLocationBase(string Name);
}
