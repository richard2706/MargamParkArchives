namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryTests
{
    private readonly string _idMismatchMsg = GetPropertyMismatchMsg(nameof(Category.Id), nameof(Category));
    private readonly string _descriptionMismatchMsg = GetPropertyMismatchMsg(nameof(Category.Description), nameof(Category));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string id = "A";
        const string description = "Test";
        Category category = new(id, description);

        Assert.AreEqual(id, category.Id, _idMismatchMsg));
        Assert.AreEqual(description, category.Description, _descriptionMismatchMsg);
    }
}
