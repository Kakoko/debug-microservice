using Microsoft.EntityFrameworkCore;
using OptInfocom.Delivery.Data.Context;
using OptInfocom.Infra.IoC;

namespace OptInfocom.Delivery.Api
{
    public static class ConfigureAllServices
    {
        public static void ConfigureSupervisor(IServiceCollection services)
        {
           // DependencyContainer.RegisterServices(services);
        }

        public static void AddConnectionProvider(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DeliveryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DeliveryDbConnection"));
            });
        }
    }
}
