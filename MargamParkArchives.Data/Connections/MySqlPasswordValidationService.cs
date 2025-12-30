using MargamParkArchives.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchives.Data.Connections;

public class MySqlPasswordValidationService : IDatabasePasswordValidationService
{
    public Task<bool> ValidatePasswordAsync(string password)
    {
        throw new NotImplementedException();
    }
}
