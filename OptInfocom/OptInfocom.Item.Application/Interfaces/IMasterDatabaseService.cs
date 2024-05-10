using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Interfaces
{
    public interface IMasterDatabaseService
    {
        string GetUserCompanyConnectionString(string appKey);
    }
}
