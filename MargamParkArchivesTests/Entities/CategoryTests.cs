using MargamParkArchives.Entities;

namespace MargamParkArchivesTests;

[TestClass]
public class CategoryTests
{
    [TestMethod]
    public void CreateCategory_GivenAllData_ContainsAllData()
    {
        const string id = "A";
        const string description = "Test";
        Category category = new(id, description);

        Assert.AreEqual(id, category.Id, "The id stored in the category does not match the id used when creating the category.");
        Assert.AreEqual(description, category.Description, "The description stored in the category does not match the description used when creating the category.");
    }
}
