using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Application.Services;
using OptInfocom.Item.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Application
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddApplicationImplementation(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IMasterDatabaseRepository, MasterDatabaseRepository>();

            //services.AddTransient<IItemRepository, ItemRepository>();

            //Application Service
            services.AddTransient<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddTransient<IMasterDatabaseService, MasterDatabaseService>();

            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IApiService, ApiService>();

            return services;
        }
    }
}
