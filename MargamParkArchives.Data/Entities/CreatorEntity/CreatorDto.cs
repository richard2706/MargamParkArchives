using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Data.Entities.CreatorEntity;

internal record CreatorDto
{
    internal required int CreatorId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }

    internal Creator ToCreator() => new(CreatorId, Name, DateCreated, DateModified);
}
