using MargamParkArchives.Entities;

using static MargamParkArchivesTests.TestMessageConstants;

namespace MargamParkArchivesTests.Entities;

[TestClass]
public class PeriodTests
{
    private const string ClassUnderTestName = "Period";

    [TestMethod]
    public void CreatePeriod_GivenAllValues_ContainsAllValues()
    {
        const int id = 1;
        const string dates = "2000 - 2010";
        Period period = new(id, dates);

        Assert.AreEqual(id, period.Id, GetPropertyMismatchMsg("id", ClassUnderTestName));
        Assert.AreEqual(dates, period.Dates, GetPropertyMismatchMsg("dates", ClassUnderTestName));
    }
}
