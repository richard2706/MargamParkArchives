using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.PeriodEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.PeriodEntity;

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

    // Negative id numbers allowed since period id can be freely chosen by user

    [Fact]
    public async Task GetOnePeriodAsync_PeriodExists_ReturnsPeriod()
    {
        const int periodId = 1;
        Period? expected = new(periodId, "1800s");

        // Set up data access mock
        const string sqlQuery = "select * from period where period_id = @PeriodId;";
        PeriodDto? periodDto = new() { PeriodId = 1, Dates = "1800s" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<PeriodDto?, object>(
                sqlQuery,
                // Check anonymous object contains PeriodId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("PeriodId")!.GetValue(p)! == periodId)))
            .ReturnsAsync(periodDto);

        // Execute reader method
        Period? actual = await _periodReader.GetOnePeriodAsync(periodId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Dates), (actual.Id, actual.Dates)); // Check property values
    }

    [Fact]
    public async Task GetOnePeriodAsync_PeriodDoesNotExist_ReturnsNull()
    {
        const int periodId = 1;

        // Set up data access mock
        const string sqlQuery = "select * from period where period_id = @PeriodId;";
        PeriodDto? periodDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<PeriodDto?, object>(
                sqlQuery,
                // Check anonymous object contains PeriodId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("PeriodId")!.GetValue(p)! == periodId)))
            .ReturnsAsync(periodDto);

        // Execute reader method
        Period? actual = await _periodReader.GetOnePeriodAsync(periodId);

        Assert.Null(actual);
    }
}
