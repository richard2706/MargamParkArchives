namespace MargamParkArchivesApp.Entities
{
    /// <summary>
    /// Represents a specific location entity from the database, excluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Id">Int id of the specific location.</param>
    /// <param name="Summary">Summary of the specific location.</param>
    internal record SpecificLocation(int Id, string Summary) : SpecificLocationBase(Summary);
}
