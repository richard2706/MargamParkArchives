namespace MargamParkArchivesTests.Entities;

using MargamParkArchivesApp.Entities;
using static TestMessageConstants;

[TestClass]
public class IdentifierGroupTests
{
    private const string LongIdNoExeptionMsg = "An id longer than 3 characters was incorrectly accepted when instatiating the IdentiferGroup.";
    private const string EmptyStringNoExeptionMsg = "An id set as the empty string was incorrectly accepted when instatiating the IdentiferGroup.";

    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(IdentifierGroup.Id), nameof(IdentifierGroup));
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(IdentifierGroup.Name), nameof(IdentifierGroup));

    [TestMethod]
    public void CreateInstance_GivenAllValidData_ContainsAllData()
    {
        const string id = "ABC";
        const string name = "Test";
        IdentifierGroup identifierGroup = new(id, name);

        Assert.AreEqual(id, identifierGroup.Id, _idMismatchMsg);
        Assert.AreEqual(name, identifierGroup.Name, _nameMismatchMsg);
    }

    [TestMethod]
    [Description("Verifies that an identifer group cannot have a key of more than 3 characters.")]
    public void CreateInstance_GivenLongId_ThrowsException()
    {
        const string id = "ABCD";
        const string name = "Test";
        Assert.ThrowsException<ArgumentException>(() => new IdentifierGroup(id, name), LongIdNoExeptionMsg);
    }

    [TestMethod]
    [Description("Verifies that an identifer group cannot have the empty string as a valid key.")]
    public void CreateInstance_GivenEmptyId_ThrowsException()
    {
        const string id = "";
        const string name = "Test";
        Assert.ThrowsException<ArgumentException>(() => new IdentifierGroup(id, name), EmptyStringNoExeptionMsg);
    }
}
