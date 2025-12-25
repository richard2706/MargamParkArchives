using MargamParkArchives.Data.Entities;
using static MargamParkArchives.Data.Tests.TestMessageConstants;

namespace MargamParkArchives.Data.Tests.Entities;

[TestClass]
public class CreatorBaseTests
{
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(CreatorBase.Name), nameof(CreatorBase));

    [TestMethod]
    public void CreateInstance_GivenAllValues_ContainsAllValues()
    {
        const string name = "Dan";
        CreatorBase creator = new(name);
        Assert.AreEqual(name, creator.Name, _nameMismatchMsg);
    }
}
