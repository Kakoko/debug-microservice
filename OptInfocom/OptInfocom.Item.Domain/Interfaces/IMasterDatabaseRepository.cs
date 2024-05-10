using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Domain.Interfaces
{
    public interface IMasterDatabaseRepository
    {
        string GetUserCompanyConnectionString(string appKey);
    }
}
