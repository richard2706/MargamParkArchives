using MargamParkArchives.Core.Entities.GeneralLocationEntity;

namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

internal record GeneralLocationDto
{
    internal required int GeneralLocationId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }

    internal GeneralLocation ToGeneralLocation() => new(GeneralLocationId, Name, DateCreated, DateModified);
}
