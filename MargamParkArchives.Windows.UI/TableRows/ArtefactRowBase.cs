using System;

namespace MargamParkArchives.Windows.UI.TableRows;

/// <summary>
/// Specifies the columns displayed at minimum in a TableView row for an Artefact
/// </summary>
/// <remarks>
/// Nullable properties specified here because we should account for missing values when displaying them (e.g. value
/// may be null if the database was manually edited)
/// </remarks>
public abstract class ArtefactRowBase
{
    public abstract string? IdentifierKey { get; }
    public abstract string? IdentifierGroupName { get; }
    public abstract string? FilePath { get; }
    public abstract DateTime? DateCreated { get; }
    public abstract string? DescriptionEn { get; }
    public abstract bool? VisualArtefact { get; }
    public abstract string? CategoryName { get; }
    public abstract string? CreatorName { get; }
    public abstract string? PeriodDates { get; }
}
