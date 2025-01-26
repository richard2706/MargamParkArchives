using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchives.Entities
{
    /// <summary>
    /// Represents a creator entity from the database, including its ID. Should be used when retriving/updating categories from the database.
    /// </summary>
    /// <param name="Id">Int id of the creator.</param>
    /// <param name="Nane">Name of the creator.</param>
    internal record Creator(int Id, string Name) : CreatorBase(Name);
}
