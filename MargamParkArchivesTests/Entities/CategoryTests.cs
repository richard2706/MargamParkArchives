namespace MargamParkArchivesTests.Entities;

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryTests
{
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
}
