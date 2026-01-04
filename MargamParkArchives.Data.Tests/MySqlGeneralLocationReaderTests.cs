using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

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
            new GeneralLocationDto() { GeneralLocationId = 1, Name = "Cabinet A" },
            new GeneralLocationDto() { GeneralLocationId = 2, Name = "Cabinet B" }
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
}
