using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Services
{
    public class MasterDatabaseService : IMasterDatabaseService
    {
        private readonly IMasterDatabaseRepository _masterDatabaseRepository;

        public MasterDatabaseService(IMasterDatabaseRepository masterDatabaseRepository)
        {
            _masterDatabaseRepository = masterDatabaseRepository;
        }

        public string GetUserCompanyConnectionString(string appKey)
        {
            var result = _masterDatabaseRepository.GetUserCompanyConnectionString(appKey);
            return result;
        }
    }
}
