namespace MargamParkArchivesDataTests.Entities;

using MargamParkArchivesData.Entities;
using static MargamParkArchivesDataTests.TestMessageConstants;

[TestClass]
public class SpecificLocationBaseTests
{
    private readonly string _summaryMismatchMsg = GetPropertyMismatchMsg(nameof(SpecificLocation.Summary), nameof(SpecificLocation));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string summary = "Filing Cabinet 1";
        SpecificLocationBase specificLocationBase = new(summary);
        Assert.AreEqual(summary, specificLocationBase.Summary, _summaryMismatchMsg);
    }
}
