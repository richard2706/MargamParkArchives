using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.SpecificLocationEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

public class MySqlSpecificLocationTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly ISpecificLocationReader _specificLocationReader; // Instance of the class under test

    public MySqlSpecificLocationTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _specificLocationReader = new MySqlSpecificLocationReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllSpecificLocationsAsync_SpecificLocationsExist_ReturnsAllSpecificLocations()
    {
        SpecificLocation[] expected =
        [
            new(1, "Shelf 1"),
            new(2, "Shelf 2")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from specific_location;";
        SpecificLocationDto[] specificLocationDtos =
        [
            new SpecificLocationDto() { SpecificLocationId = 1, Summary = "Shelf 1" },
            new SpecificLocationDto() { SpecificLocationId = 2, Summary = "Shelf 2" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<SpecificLocationDto>(sqlQuery))
            .ReturnsAsync(specificLocationDtos);

        // Execute reader method
        SpecificLocation[] actual = await _specificLocationReader.GetAllSpecificLocationsAsync();

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal( // Assert actual contains specific location with correct property values in any order
            expected.Select(specificLocation => (specificLocation.Id, specificLocation.Summary)).OrderBy(item => item.Id),
            actual.Select(specificLocation => (specificLocation.Id, specificLocation.Summary)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllSpecificLocationsAsync_NoSpecificLocationsExist_ReturnsEmptyArray()
    {
        SpecificLocation[] expectedSpecificLocations = [];

        // Set up data access mock
        const string sqlQuery = "select * from specific_location;";
        SpecificLocationDto[] specificLocationDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<SpecificLocationDto>(sqlQuery))
            .ReturnsAsync(specificLocationDtos);

        // Execute reader method
        SpecificLocation[] actual = await _specificLocationReader.GetAllSpecificLocationsAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetOneSpecificLocationAsync_InvalidId_ThrowsException()
    {
        const int invalidId = -1; // Id cannot be less than 0
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _specificLocationReader.GetOneSpecificLocationAsync(invalidId));
    }

    [Fact]
    public async Task GetOneSpecificLocationAsync_SpecificLocationExists_ReturnsSpecificLocation()
    {
        const int specificLocationId = 1;
        SpecificLocation? expected = new(specificLocationId, "Shelf 1");

        // Set up data access mock
        const string sqlQuery = "select * from specific_location where specific_location_id = @SpecificLocationId;";
        SpecificLocationDto? specificLocationDto = new() { SpecificLocationId = 1, Summary = "Shelf 1" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<SpecificLocationDto?, object>(
                sqlQuery,
                // Check anonymous object contains SpecificLocationId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("SpecificLocationId")!.GetValue(p)! == specificLocationId)))
            .ReturnsAsync(specificLocationDto);

        // Execute reader method
        SpecificLocation? actual = await _specificLocationReader.GetOneSpecificLocationAsync(specificLocationId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Summary), (actual.Id, actual.Summary)); // Check property values
    }

    [Fact]
    public async Task GetOneSpecificLocationAsync_SpecificLocationDoesNotExist_ReturnsNull()
    {
        const int specificLocationId = 1;

        // Set up data access mock
        const string sqlQuery = "select * from specific_location where specific_location_id = @SpecificLocationId;";
        SpecificLocationDto? specificLocationDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<SpecificLocationDto?, object>(
                sqlQuery,
                // Check anonymous object contains SpecificLocationId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("SpecificLocationId")!.GetValue(p)! == specificLocationId)))
            .ReturnsAsync(specificLocationDto);

        // Execute reader method
        SpecificLocation? actual = await _specificLocationReader.GetOneSpecificLocationAsync(specificLocationId);

        Assert.Null(actual);
    }
}
