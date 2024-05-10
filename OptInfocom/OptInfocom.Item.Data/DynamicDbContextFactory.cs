using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Data
{
    public class DynamicDbContextFactory
    {
        public readonly string _masterConnectionString;

        public DynamicDbContextFactory(IConfiguration configuration)
        {
            _masterConnectionString = configuration.GetConnectionString("MyConnectionString");
        }
    }
}
