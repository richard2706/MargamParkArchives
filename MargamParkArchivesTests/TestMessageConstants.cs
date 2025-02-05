using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchivesDataTests
{
    internal static class TestMessageConstants
    {
        private const string PropertyMismatchMsg = "The '{0}' stored in the '{1}' instance does not match the '{0}' used when creating the '{1}'.";

        internal static string GetPropertyMismatchMsg(string propertyName, string entityName) =>
            string.Format(PropertyMismatchMsg, propertyName, entityName);
    }
}
