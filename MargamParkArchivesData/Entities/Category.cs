using System;

namespace MargamParkArchivesData.Entities
{
    /// <summary>
    /// Represents a category entity from the database, including its ID. Should be used when retriving/updating categories from the database.
    /// </summary>
    public record Category
    {
        private const int MaxIdLength = 2;
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
                else if (value.Length > MaxIdLength)
                {
                    throw new ArgumentException(IdTooLongErrorMsg);
                }
                _id = value;
            }
        }

        public string Name { get; init; }

        /// <summary>
        /// Create a category with an id and name.
        /// </summary>
        /// <param name="Id">Int id of the category.</param>
        /// <param name="Name">Name of the category.</param>
        public Category(string id, string name)
        {
            _id = id;
            Id = id;
            Name = name;
        }
    }
}
