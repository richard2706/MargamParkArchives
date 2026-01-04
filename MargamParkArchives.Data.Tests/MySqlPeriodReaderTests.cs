using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.PeriodEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

public class MySqlPeriodReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly IPeriodReader _periodReader; // Instance of the class under test

    public MySqlPeriodReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _periodReader = new MySqlPeriodReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllPeriodsAsync_PeriodsExist_ReturnsAllPeriods()
    {
        Period[] expectedPeriods =
        [
            new(1, "1800s"),
            new(2, "1900s")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from period;";
        PeriodDto[] periodDtos =
        [
            new PeriodDto() { PeriodId = 1, Dates = "1800s" },
            new PeriodDto() { PeriodId = 2, Dates = "1900s" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<PeriodDto>(sqlQuery))
            .ReturnsAsync(periodDtos);

        // Execute reader method
        Period[] actual = await _periodReader.GetAllPeriodsAsync();

        Assert.Equal(expectedPeriods.Length, actual.Length);
        Assert.Equal( // Assert actual contains periods with correct property values in any order
            expectedPeriods.Select(period => (period.Id, period.Dates)).OrderBy(item => item.Id),
            actual.Select(period => (period.Id, period.Dates)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllPeriodsAsync_NoPeriodsExist_ReturnsEmptyArray()
    {
        Period[] expectedPeriods = [];

        // Set up data access mock
        const string sqlQuery = "select * from period;";
        PeriodDto[] periodDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<PeriodDto>(sqlQuery))
            .ReturnsAsync(periodDtos);

        // Execute reader method
        Period[] actual = await _periodReader.GetAllPeriodsAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }
}
