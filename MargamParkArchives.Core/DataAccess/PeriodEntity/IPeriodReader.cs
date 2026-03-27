using MargamParkArchives.Core.Entities.PeriodEntity;

namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public interface IPeriodReader
{
    /// <summary>
    /// Returns an array of all periods in the database. The array will be empty if the database contains no periods.
    /// </summary>
    /// <returns>An array of all periods in the database. The array will be empty if the database contains no periods.</returns>
    public Task<Period[]> GetAllPeriodsAsync();

    /// <summary>
    /// Returns one period from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the period.</param>
    /// <returns>The period identified by the given id, or null if it doesn't exist.</returns>
    public Task<Period?> GetOnePeriodAsync(int id);

    /// <summary>
    /// Returns true if a period with the provided id exists in the database.
    /// </summary>
    /// <remarks>Note that the id is freely chosen by the user so negative numbers are valid ids.</remarks>
    /// <param name="id">Id of the period to check</param>
    /// <returns>True if the period identified by the provided id exists</returns>
    public Task<bool> PeriodExists(int id);
}
