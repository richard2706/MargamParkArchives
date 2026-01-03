using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Entities.Artefact;
using MargamParkArchives.Data.Connections;
using Moq;

namespace MargamParkArchives.Data.Tests
{
    public class MySqlArtefactDetailsReaderTests
    {
        private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
        private readonly IArtefactDetailsReader _artefactDetailsReader; // Instance of the class under test

        public MySqlArtefactDetailsReaderTests()
        {
            _dataAccessMock = new Mock<IMySqlDataAccess>();
            _artefactDetailsReader = new MySqlArtefactDetailsReader(_dataAccessMock.Object);
        }

        //  results if num artefacts requested is greater than number available OR at boundary (2 available, 2 requested) OR normal (3 available, 2 requested)

        [Fact]
        public async Task GetRandomArtefactsAsync_OneExistsOneRequested_ReturnsArtefactsArray()
        {
            ArtefactDetailsReadModel[] expectedArtefacts = // Method under test should return read models
            [
                new("A", 1, "A-000001", "Apple")
            ];

            // Set up data access mock
            const int numArtefactsRequested = 1;
            const string sqlQuery = "select * from artefact_details order by rand() limit @Limit;";
            ArtefactDetailsDto[] randomArtefactDetailsDtos = // Data access mocked method returns random Dto types
            [
                new ArtefactDetailsDto() { IdentifierGroupId = "A", IdentifierNumber = 1, IdentifierKey = "A-000001", IdentiferGroupName = "Apple" }
            ];
            _dataAccessMock
                .Setup(x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(
                    sqlQuery,
                    // Checks the anonymous object contains a property called Limit that is set to numArtefactsRequested
                    It.Is<object>(p => (int)p.GetType().GetProperty("Limit")!.GetValue(p)! == numArtefactsRequested)
                    ))
                .ReturnsAsync(randomArtefactDetailsDtos);

            ArtefactDetailsReadModel[] actual = await _artefactDetailsReader.GetRandomArtefactsAsync(numArtefactsRequested);

            // Verify data access method was called exactly once
            _dataAccessMock.Verify(x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(
                sqlQuery, It.Is<object>(p => (int)p.GetType().GetProperty("Limit")!.GetValue(p)! == numArtefactsRequested)),
                Times.Once);

            Assert.Single(actual); // Array should contain one item
            Assert.Equal(expectedArtefacts, actual);
        }
    }
}