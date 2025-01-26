namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class SpecificLocationTests
{
    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(SpecificLocation.Id), nameof(SpecificLocation));
    private readonly string _summaryMismatchMsg = GetPropertyMismatchMsg(nameof(SpecificLocation.Summary), nameof(SpecificLocation));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const int id = 1;
        const string summary = "Filing Cabinet 1";
        SpecificLocation specificLocation = new(id, summary);

        Assert.AreEqual(id, specificLocation.Id, _idMismatchMsg);
        Assert.AreEqual(summary, specificLocation.Summary, _summaryMismatchMsg);
    }
}
