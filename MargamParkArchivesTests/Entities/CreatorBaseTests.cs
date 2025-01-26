namespace MargamParkArchivesTests;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CreatorBaseTests
{
    private const string ClassUnderTestName = "CreatorBase";

    [TestMethod]
    public void CreateCreatorBase_GivenAllValues_ContainsAllValues()
    {
        const string name = "Dan";
        CreatorBase creator = new(name);
        Assert.AreEqual(name, creator.Name, GetPropertyMismatchMsg("name", ClassUnderTestName));
    }
}
