using MargamParkArchives.Entities;

using static MargamParkArchivesTests.TestMessageConstants;

namespace MargamParkArchivesTests.Entities;

[TestClass]
public class CategoryTests
{
    private const string ClassUnderTestName = "Category";

    [TestMethod]
    public void CreateCategory_GivenAllData_ContainsAllData()
    {
        const string id = "A";
        const string description = "Test";
        Category category = new(id, description);

        Assert.AreEqual(id, category.Id, GetPropertyMismatchMsg("id", ClassUnderTestName));
        Assert.AreEqual(description, category.Description, GetPropertyMismatchMsg("description", ClassUnderTestName));
    }
}
