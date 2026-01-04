using MargamParkArchives.Core.Database.DataAccess;
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
}
