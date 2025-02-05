using System;

namespace MargamParkArchivesData.Entities
{
    /// <summary>
    /// Represents an identifer group entity from the database.
    /// </summary>
    public record IdentifierGroup
    {
        private const int MaxIdLength = 3;
        private const string IdEmptyErrorMsg = "The Id cannot be an empty string.";
        private readonly string IdTooLongErrorMsg = string.Format("The Id cannot be longer than {0} characters", MaxIdLength);

        private readonly string _id;
        public string Id
        {
            get => _id;
            init
            {
                if (value.Length <= 0)
                {
                    throw new ArgumentException(IdEmptyErrorMsg);
                }
                else if (value.Length > 3)
                {
                    throw new ArgumentException(IdTooLongErrorMsg);
                }
                _id = value;
            }
        }

        public string Name { get; init; }

        public IdentifierGroup(string id, string name)
        {
            _id = id;
            Id = id;
            Name = name;
        }
    }
}
