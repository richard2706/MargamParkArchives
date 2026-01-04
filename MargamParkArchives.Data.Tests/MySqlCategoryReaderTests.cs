using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

public class MySqlCategoryReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly ICategoryReader _categoryReader; // Instance of the class under test

    public MySqlCategoryReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _categoryReader = new MySqlCategoryReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_CategoriesExist_ReturnsAllCategories()
    {
        Category[] expectedCategories =
        [
            new("A", "Apple"),
            new("B", "Banana")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from category;";
        CategoryDto[] categoryDtos =
        [
            new CategoryDto() { CategoryId = "A", Name = "Apple" },
            new CategoryDto() { CategoryId = "B", Name = "Banana" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<CategoryDto>(sqlQuery))
            .ReturnsAsync(categoryDtos);

        // Execute reader method
        Category[] actual = await _categoryReader.GetAllCategoriesAsync();

        Assert.Equal(expectedCategories.Length, actual.Length);
        Assert.Equal( // Assert actual contains categories with correct property values in any order
            expectedCategories.Select(category => (category.Id, category.Name)).OrderBy(item => item.Id),
            actual.Select(category => (category.Id, category.Name)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllCategoriesAsync_NoCategoriesExist_ReturnsEmptyArray()
    {
        Category[] expectedCategories = [];

        // Set up data access mock
        const string sqlQuery = "select * from category;";
        CategoryDto[] categoryDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<CategoryDto>(sqlQuery))
            .ReturnsAsync(categoryDtos);

        // Execute reader method
        Category[] actual = await _categoryReader.GetAllCategoriesAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }
}
