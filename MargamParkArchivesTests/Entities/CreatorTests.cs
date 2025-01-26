namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CreatorTests
{
    private const string ClassUnderTestName = "Creator";

    [TestMethod]
    public void CreateCreator_GivenAllValues_ContainsAllValues()
    {
        const int id = 1;
        const string name = "Dan";
        Creator creator = new(id, name);

        Assert.AreEqual(id, creator.Id, GetPropertyMismatchMsg("id", ClassUnderTestName));
        Assert.AreEqual(name, creator.Name, GetPropertyMismatchMsg("name", ClassUnderTestName));
    }
}
