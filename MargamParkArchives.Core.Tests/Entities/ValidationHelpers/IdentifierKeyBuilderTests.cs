using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Tests.Entities.ValidationHelpers;

public class IdentifierKeyBuilderTests
{
    [Theory]
    [InlineData("CIS-000001", "CIS", 1)]
    [InlineData("CIS-201984", "CIS", 201984)]
    [InlineData("WW2-000001", "WW2", 1)]
    public void BuildsCorrectIdentiferKey_FromValidIdAndNumber(string expectedIdentifierKey,
        string identiferGroupId, int identiferNumber)
    {
        string actualIdentifierKey = IdentifierKeyBuilder.Build(identiferGroupId, identiferNumber);
        Assert.Equal(expectedIdentifierKey, actualIdentifierKey);
    }
}
