using MargamParkArchives.Data.Entities;
using static MargamParkArchives.Data.Tests.TestMessageConstants;

namespace MargamParkArchives.Data.Tests.Entities;

[TestClass]
public class GeneralLocationBaseTests
{
    private readonly string _propertyMismatchMsg = GetPropertyMismatchMsg(nameof(GeneralLocationBase.Name), nameof(GeneralLocationBase));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string name = "Room 1A";
        GeneralLocationBase generalLocationBase = new(name);
        Assert.AreEqual(name, generalLocationBase.Name, _propertyMismatchMsg);
    }
}
