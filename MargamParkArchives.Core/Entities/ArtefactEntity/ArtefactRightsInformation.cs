namespace MargamParkArchives.Core.Entities.ArtefactEntity;

public class ArtefactRightsInformation
{
    public string? RightType1 { get; }
    public string? RightHolder1En { get; }
    public string? RightHolder1Cy { get; }

    public ArtefactRightsInformation(string? rightType1, string? rightHolder1En, string? rightHolder1Cy)
    {
        if (!ArtefactRightsInformationValidationRules.IsValidRightsInformation(rightType1, nameof(rightType1), out string error))
        {
            throw new ArgumentException(error, nameof(rightType1));
        }
        else if (!ArtefactRightsInformationValidationRules.IsValidRightsInformation(rightHolder1En, nameof(rightHolder1En), out error))
        {
            throw new ArgumentException(error, nameof(rightHolder1En));
        }
        else if (!ArtefactRightsInformationValidationRules.IsValidRightsInformation(rightHolder1Cy, nameof(rightHolder1Cy), out error))
        {
            throw new ArgumentException(error, nameof(rightHolder1Cy));
        }

        RightType1 = rightType1;
        RightHolder1En = rightHolder1En;
        RightHolder1Cy = rightHolder1Cy;
    }
}
