namespace MargamParkArchives.Entities
{
    /// <summary>
    /// Represents a general location entity from the database, exluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Id">Int id of the general location.</param>
    /// <param name="Name">Name of the general location.</param>
    internal record GeneralLocation(int Id, string Name) : GeneralLocationBase(Name);
}
