using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using MargamParkArchives.Data.Entities.CategoryEntity;
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

    [Fact]
    public async Task GetOneCategoryAsync_InvalidId_ThrowsException()
    {
        const string invalidId = ""; // Id cannot be an empty string
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _categoryReader.GetOneCategoryAsync(invalidId));
    }

    [Fact]
    public async Task GetOneCategoryAsync_CategoryExists_ReturnsCategory()
    {
        const string categoryId = "A";
        Category? expected = new(categoryId, "Apple");

        // Set up data access mock
        const string sqlQuery = "select * from category where category_id = @CategoryId;";
        CategoryDto? categoryDto = new() { CategoryId = "A", Name = "Apple" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<CategoryDto?, object>(
                sqlQuery,
                // Check anonymous object contains CategoryId property with correct value
                It.Is<object>(o => o.GetType().GetProperty("CategoryId")!.GetValue(o)!.Equals(categoryId))))
            .ReturnsAsync(categoryDto);

        // Execute reader method
        Category? actual = await _categoryReader.GetOneCategoryAsync(categoryId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Name), (actual.Id, actual.Name)); // Check property values
    }

    [Fact]
    public async Task GetOneCategoryAsync_CategoryDoesNotExist_ReturnsNull()
    {
        const string categoryId = "A";

        // Set up data access mock
        const string sqlQuery = "select * from category where category_id = @CategoryId;";
        CategoryDto? categoryDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<CategoryDto?, object>(
                sqlQuery,
                // Check anonymous object contains CategoryId property with correct value
                It.Is<object>(o => o.GetType().GetProperty("CategoryId")!.GetValue(o)!.Equals(categoryId))))
            .ReturnsAsync(categoryDto);

        // Execute reader method
        Category? actual = await _categoryReader.GetOneCategoryAsync(categoryId);

        Assert.Null(actual);
    }
}
