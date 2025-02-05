namespace MargamParkArchivesApp.Entities
{
    /// <summary>
    /// Represents a general location entity from the database, exluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Name">Name of the general location.</param>
    internal record GeneralLocationBase(string Name);
}
