using MargamParkArchives.Entities;

namespace MargamParkArchivesTests.Entities;

[TestClass]
public class CategoryBaseTests
{
    [TestMethod]
    public void CreateCategory_GivenAllData_ContainsAllData()
    {
        const string description = "Test";
        CategoryBase category = new(description);
        Assert.AreEqual(description, category.Description);
    }
}
