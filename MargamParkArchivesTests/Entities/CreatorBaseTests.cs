namespace MargamParkArchivesTests.Entities;

using MargamParkArchivesApp.Entities;
using static TestMessageConstants;

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
