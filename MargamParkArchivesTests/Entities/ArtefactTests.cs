using MargamParkArchives.Entities;

namespace MargamParkArchivesTests.Entities
{
    [TestClass]
    public sealed class ArtefactTests
    {
        [TestMethod]
        [Description("This test verifies that the test method in the Artefact class returns 0.")]
        public void TestMethod_AnyState_Returns0()
        {
            const int expected = 0;
            Artefact artefact = new();
            int actual = artefact.TestMethod();
            Assert.AreEqual(expected, actual);
        }
    }
}
