using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;

namespace MargamParkArchives.Core.DataAccess.PeriodEntity;

public interface IPeriodWriter
{
    /// <summary>
    /// Creates a new period item in the database asynchronously.
    /// </summary>
    /// <param name="period">Object containing the values for the new period item.</param>
    /// <returns>The id of the newly created period item</returns>
    public Task<int> CreatePeriodAsync(PeriodCreateDto period);

    /// <summary>
    /// Updates an existing period item in the database asynchronously.
    /// </summary>
    /// <param name="period">Object containing the updated values for the period.</param>
    /// <returns>True if the period was updated successfully or false if the period was not found.</returns>
    public Task<bool> UpdatePeriodAsync(PeriodUpdateDto period);
}
