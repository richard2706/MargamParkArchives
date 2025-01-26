namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryTests
{
    private const string LongIdNoExeptionMsg = "An id longer than 3 characters was incorrectly accepted when instatiating the IdentiferGroup.";
    private const string EmptyStringNoExeptionMsg = "An id set as the empty string was incorrectly accepted when instatiating the IdentiferGroup.";

    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(Category.Id), nameof(Category));
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(Category.Name), nameof(Category));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string id = "A";
        const string name = "Test";
        Category category = new(id, name);

        Assert.AreEqual(id, category.Id, _idMismatchMsg);
        Assert.AreEqual(name, category.Name, _nameMismatchMsg);
    }

    [TestMethod]
    [Description("Verifies that a category cannot have a key of more than 2 characters.")]
    public void CreateInstance_GivenLongId_ThrowsException()
    {
        const string id = "ABC";
        const string name = "Test";
        Assert.ThrowsException<ArgumentException>(() => new Category(id, name), LongIdNoExeptionMsg);
    }

    [TestMethod]
    [Description("Verifies that a category cannot have the empty string as a valid key.")]
    public void CreateInstance_GivenEmptyId_ThrowsException()
    {
        const string id = "";
        const string name = "Test";
        Assert.ThrowsException<ArgumentException>(() => new Category(id, name), EmptyStringNoExeptionMsg);
    }
}
