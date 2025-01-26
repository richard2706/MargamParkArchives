using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchives.Entities
{
    /// <summary>
    /// Represents a category entity from the database, exluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Name">Description of the category.</param>
    internal record CategoryBase(string Name);
}
