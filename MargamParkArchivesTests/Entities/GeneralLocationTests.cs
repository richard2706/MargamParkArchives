namespace MargamParkArchivesTests;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class GeneralLocationTests
{
    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(GeneralLocation.Id), nameof(GeneralLocation));
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(GeneralLocation.Name), nameof(GeneralLocation));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const int id = 1;
        const string name = "Room 1A";
        GeneralLocation generalLocationBase = new(id, name);

        Assert.AreEqual(id, generalLocationBase.Id, _idMismatchMsg);
        Assert.AreEqual(name, generalLocationBase.Name, _nameMismatchMsg);
    }
}
