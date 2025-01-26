using MargamParkArchives.Entities;

namespace MargamParkArchivesTests;

[TestClass]
public class PeriodTests
{
    private const string ClassUnderTestName = "period";
    private const string PropertyMismatchFailMsg = "The {0} stored in the {1} instance does not match the {0} used when creating the {1}";

    [TestMethod]
    public void CreatePeriod_GivenAllValues_ContainsAllValues()
    {
        const int id = 1;
        const string dates = "2000 - 2010";
        Period period = new(id, dates);

        Assert.AreEqual(id, period.Id, string.Format(PropertyMismatchFailMsg, "id", ClassUnderTestName));
        Assert.AreEqual(dates, period.Dates, string.Format(PropertyMismatchFailMsg, "dates", ClassUnderTestName));
    }
}
