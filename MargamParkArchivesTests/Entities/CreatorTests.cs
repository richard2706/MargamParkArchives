namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CreatorTests
{
    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(Creator.Id), nameof(Creator));
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(Creator.Name), nameof(Creator));

    [TestMethod]
    public void CreateInstance_GivenAllValues_ContainsAllValues()
    {
        const int id = 1;
        const string name = "Dan";
        Creator creator = new(id, name);

        Assert.AreEqual(id, creator.Id, _idMismatchMsg);
        Assert.AreEqual(name, creator.Name, _nameMismatchMsg);
    }
}
