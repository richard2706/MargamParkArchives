namespace MargamParkArchivesData.Entities;

/// <summary>
/// Represents a specific location entity from the database, excluding its ID. Should be used when inserting into the database.
/// </summary>
/// <param name="summary">Summary of the specific location.</param>
public record SpecificLocationBase(string Summary);
