using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchives.Entities
{
    /// <summary>
    /// Represents a category entity from the database, including its ID. Should be used when retriving/updating categories from the database.
    /// </summary>
    /// <param name="Id">Int id of the category.</param>
    /// <param name="Name">Description of the category.</param>
    internal record Category(string Id, string Name) : CategoryBase(Name);
}
