using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Data.Context;
using OptInfocom.Item.Data.Repository;
using OptInfocom.Item.Domain.Interfaces;

namespace OptInfocom.Item.Data
{
    public static class AddDataServices
    {
        public static IServiceCollection AddDataServicesImplementation(this IServiceCollection services, IConfiguration configuration)
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

            services.AddTransient<IMasterDatabaseRepository, MasterDatabaseRepository>();

            services.AddTransient<IItemRepository, ItemRepository>();

            return services;

        }
    }
}
