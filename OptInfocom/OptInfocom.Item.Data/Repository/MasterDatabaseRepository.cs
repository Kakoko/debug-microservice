using OptInfocom.Item.Data.Context;
using OptInfocom.Item.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Data.Repository
{
    public class MasterDatabaseRepository : IMasterDatabaseRepository
    {
        private MasterDbContext _ctx;

        public MasterDatabaseRepository(MasterDbContext ctx)
        {
            _ctx = ctx;
        }

        public string GetUserCompanyConnectionString(string appKey)
        {
            var division = _ctx.Divisions.Where(u => u.tenant_code == appKey).SingleOrDefault();
            //var result = _ctx.Companies.Where(u => u.app_key == appKey).Include(p => p.Divisions).SingleOrDefault();

            //var division = result.Divisions.First();

            var serverName = division.db_server_name.Replace("\\\\", "\\");
            // Construct the connection string
            //"Server=BT-FSR-SKALUWA\\SQLEXPRESS01;Database=OptErp;Trusted_Connection=True;TrustServerCertificate=True;"
            var connectionString = $"Server={division.db_server_name};Database={division.db_database_name};User={division.db_user_name};Password={division.db_server_password};Trusted_Connection=false;TrustServerCertificate=True;";
            //var connectionString = $"Server={division.db_server_name};Database={division.db_database_name};Trusted_Connection=True;TrustServerCertificate=True;";
            connectionString = connectionString.Replace("\\\\", "\\");
            return connectionString;

            // return result;
        }


    }
}
