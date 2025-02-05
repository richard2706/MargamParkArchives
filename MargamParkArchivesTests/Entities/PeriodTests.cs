namespace MargamParkArchivesTests.Entities;

using MargamParkArchivesApp.Entities;
using static TestMessageConstants;

[TestClass]
public class PeriodTests
{
    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(Period.Id), nameof(Period));
    private readonly string _datesMismatchMsg = GetPropertyMismatchMsg(nameof(Period.Dates), nameof(Period));

    [TestMethod]
    public void CreateInstance_GivenAllValues_ContainsAllValues()
    {
        const int id = 1;
        const string dates = "2000 - 2010";
        Period period = new(id, dates);

        Assert.AreEqual(id, period.Id, _idMismatchMsg);
        Assert.AreEqual(dates, period.Dates, _datesMismatchMsg);
    }
}
