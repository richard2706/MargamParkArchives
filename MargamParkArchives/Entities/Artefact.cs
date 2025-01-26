using System;

namespace MargamParkArchives.Entities
{
    internal record Artefact : ArtefactBase
    {
        public int IdentifierNumber { get; init; }
        public int IdentifierKey { get; init; }
        public DateTime DateCreated { get; init; }
        public DateTime DateModified { get; init; }
    }
}
