using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Delivery.Application.Interfaces;
using OptInfocom.Delivery.Application.Services;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Application
{
    public static class DeliveryApplicationServices
    {
        public static IServiceCollection AddDeliveryApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
          //  services.AddHttpClient();

            //Application Service
            services.AddTransient<IDeliveryStatusService, DeliveryStatusService>();
            services.AddTransient<ISalesInvoiceService, SalesInvoiceService>();
            services.AddTransient<IDeliveryApiService, DeliveryApiService>();

            return services;
        }
    }
}
