using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchives.Entities
{
    /// <summary>
    /// Represents a creator entity from the database, exluding its ID. Should be used when inserting into the database.
    /// </summary>
    /// <param name="Name">Name of the creator.</param>
    internal record CreatorBase(string Name);
}
