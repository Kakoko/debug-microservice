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
        public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MasterDbConnection"));
            });

            services.AddDbContext<ItemDbContext>((serviceProvider, options) =>
            {
                //For : IHttpContextAccessor
                //https://learn.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/6.0/microsoft-aspnetcore-http-features-package-split

                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var divisionCode = httpContextAccessor.HttpContext?.Request.Headers["appkey"].FirstOrDefault();
                var tenant = httpContextAccessor.HttpContext?.Request.Headers["tenant"].FirstOrDefault();

                var connectionStringProvider = serviceProvider.GetRequiredService<IMasterDatabaseService>();
                var connectionString = connectionStringProvider.GetUserCompanyConnectionString(divisionCode);
              //  var connectionString = connectionStringProvider.GetUserCompanyConnectionString(tenant);

                options.UseSqlServer(connectionString);
            });

            services.AddTransient<DeliveryDbContext>();

            //Application Service
            services.AddTransient<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddTransient<IMasterDatabaseService, MasterDatabaseService>();

            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IApiService, ApiService>();

            //services.AddTransient<IDeliveryStatusService, DeliveryStatusService>();
            //services.AddTransient<IItemService, ItemService>();
            //services.AddTransient<ISalesInvoiceService, SalesInvoiceService>();

            //Data
            services.AddTransient<IMasterDatabaseRepository, MasterDatabaseRepository>();

            services.AddTransient<IItemRepository, ItemRepository>();

            //services.AddTransient<IDeliveryStatusRepository, DeliveryStatusRepository>();
            //services.AddTransient<IItemRepository, ItemRepository>();
            //services.AddTransient<ISalesInvoiceRepository, SalesInvoiceRepository>();


            return services;
        }

        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus

            //Application Service
            services.AddTransient<IDeliveryStatusService, DeliveryStatusService>();
            services.AddTransient<ISalesInvoiceService, SalesInvoiceService>();

            //Data
            services.AddTransient<IDeliveryStatusRepository, DeliveryStatusRepository>();
            services.AddTransient<ISalesInvoiceRepository, SalesInvoiceRepository>();
            services.AddTransient<DeliveryDbContext>();
        }
    }
}
