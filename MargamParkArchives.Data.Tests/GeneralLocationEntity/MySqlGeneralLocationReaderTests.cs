using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.GeneralLocationEntity;

public class MySqlGeneralLocationReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly IGeneralLocationReader _generalLocationReader; // Instance of the class under test

    public MySqlGeneralLocationReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _generalLocationReader = new MySqlGeneralLocationReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllGeneralLocationsAsync_GeneralLocationsExist_ReturnsAllGeneralLocations()
    {
        GeneralLocation[] expected =
        [
            new(1, "Cabinet A"),
            new(2, "Cabinet B")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from general_location;";
        GeneralLocationDto[] generalLocationDtos =
        [
            new GeneralLocationDto() { general_location_id = 1, name = "Cabinet A" },
            new GeneralLocationDto() { general_location_id = 2, name = "Cabinet B" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<GeneralLocationDto>(sqlQuery))
            .ReturnsAsync(generalLocationDtos);

        // Execute reader method
        GeneralLocation[] actual = await _generalLocationReader.GetAllGeneralLocationsAsync();

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal( // Assert actual contains general location with correct property values in any order
            expected.Select(generalLocation => (generalLocation.Id, generalLocation.Name)).OrderBy(item => item.Id),
            actual.Select(generalLocation => (generalLocation.Id, generalLocation.Name)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllGeneralLocationsAsync_NoGeneralLocationsExist_ReturnsEmptyArray()
    {
        GeneralLocation[] expectedGeneralLocations = [];

        // Set up data access mock
        const string sqlQuery = "select * from general_location;";
        GeneralLocationDto[] generalLocationDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<GeneralLocationDto>(sqlQuery))
            .ReturnsAsync(generalLocationDtos);

        // Execute reader method
        GeneralLocation[] actual = await _generalLocationReader.GetAllGeneralLocationsAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetOneGeneralLocationAsync_InvalidId_ThrowsException()
    {
        const int invalidId = -1; // Id cannot be less than 0
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _generalLocationReader.GetOneGeneralLocationAsync(invalidId));
    }

    [Fact]
    public async Task GetOneGeneralLocationAsync_GeneralLocationExists_ReturnsGeneralLocation()
    {
        const int generalLocationId = 1;
        GeneralLocation? expected = new(generalLocationId, "Cabinet A");

        // Set up data access mock
        const string sqlQuery = "select * from general_location where general_location_id = @GeneralLocationId;";
        GeneralLocationDto? generalLocationDto = new() { general_location_id = 1, name = "Cabinet A" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<GeneralLocationDto?, object>(
                sqlQuery,
                // Check anonymous object contains GeneralLocationId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("GeneralLocationId")!.GetValue(p)! == generalLocationId)))
            .ReturnsAsync(generalLocationDto);

        // Execute reader method
        GeneralLocation? actual = await _generalLocationReader.GetOneGeneralLocationAsync(generalLocationId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Name), (actual.Id, actual.Name)); // Check property values
    }

    [Fact]
    public async Task GetOneGeneralLocationAsync_GeneralLocationDoesNotExist_ReturnsNull()
    {
        const int generalLocationId = 1;

        // Set up data access mock
        const string sqlQuery = "select * from general_location where general_location_id = @GeneralLocationId;";
        GeneralLocationDto? generalLocationDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<GeneralLocationDto?, object>(
                sqlQuery,
                // Check anonymous object contains GeneralLocationId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("GeneralLocationId")!.GetValue(p)! == generalLocationId)))
            .ReturnsAsync(generalLocationDto);

        // Execute reader method
        GeneralLocation? actual = await _generalLocationReader.GetOneGeneralLocationAsync(generalLocationId);

        Assert.Null(actual);
    }
}
