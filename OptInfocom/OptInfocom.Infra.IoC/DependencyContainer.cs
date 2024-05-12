using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Delivery.Application.Interfaces;
using OptInfocom.Delivery.Application.Services;
using OptInfocom.Delivery.Data.Context;
using OptInfocom.Delivery.Data.Repository;
using OptInfocom.Delivery.Domain.Interfaces;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Application.Services;
using OptInfocom.Item.Data.Context;
using OptInfocom.Item.Data.Repository;
using OptInfocom.Item.Domain.Interfaces;
//using OptInfocom.Order.Application.Interfaces;
//using OptInfocom.Order.Application.Services;
//using OptInfocom.Order.Data.Context;
//using OptInfocom.Order.Data.Repository;
//using OptInfocom.Order.Domain.Interfaces;

namespace OptInfocom.Infra.IoC
{
    public static class DependencyContainer
    {
        //public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration)
        //{
            

        //  //  services.AddTransient<DeliveryDbContext>();


        //  //  return services;
        //}

        //public static void RegisterServices(IServiceCollection services)
        //{
        //    //Domain Bus
        //   // services.AddHttpClient();
        //    ////Application Service
        //    //services.AddTransient<IDeliveryStatusService, DeliveryStatusService>();
        //    //services.AddTransient<ISalesInvoiceService, SalesInvoiceService>();
        //    //services.AddTransient<IDeliveryApiService, DeliveryApiService>();

        //    //Data
        //    //services.AddTransient<IDeliveryStatusRepository, DeliveryStatusRepository>();
        //    //services.AddTransient<ISalesInvoiceRepository, SalesInvoiceRepository>();
        //    //services.AddTransient<DeliveryDbContext>();
        //}
    }
}
