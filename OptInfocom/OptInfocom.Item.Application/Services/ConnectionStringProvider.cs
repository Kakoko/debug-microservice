using Microsoft.Extensions.Configuration;
using OptInfocom.Item.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {

        private readonly IConfiguration _configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString(string headerValue)
        {
            throw new NotImplementedException();
        }
    }
}
