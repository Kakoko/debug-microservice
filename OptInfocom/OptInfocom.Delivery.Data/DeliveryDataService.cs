using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Delivery.Data.Context;
using OptInfocom.Delivery.Data.Repository;
using OptInfocom.Delivery.Domain.Interfaces;
using System.Net.Http;

namespace OptInfocom.Delivery.Data
{
    public static class AddDeliveryDataService
    {
        public static IServiceCollection AddDeliveryDataServicesImplementation(this IServiceCollection services, IConfiguration configuration)
        {

           

            //Data
            services.AddTransient<IDeliveryStatusRepository, DeliveryStatusRepository>();
            services.AddTransient<ISalesInvoiceRepository, SalesInvoiceRepository>();
            services.AddTransient<DeliveryDbContext>();
            return services;

        }
    }
}

