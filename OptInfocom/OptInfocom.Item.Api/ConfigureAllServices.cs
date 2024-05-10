using Microsoft.EntityFrameworkCore;
using OptInfocom.Infra.IoC;
using OptInfocom.Item.Data.Context;

namespace OptInfocom.Item.Api
{
    public class ConfigureAllServices
    {
        //public static void ConfigureSupervisor(IServiceCollection services)
        //{
        //    DependencyContainer.RegisterServices(services);
        //}

        //public static void AddConnectionProvider(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContextPool<ItemDbContext>(options =>
        //    {
        //        options.UseSqlServer(configuration.GetConnectionString("ItemDbConnection"));
        //    });

        //    //services.AddDbContextFactory<DatabaseDbContext>();

        //    services.AddDbContextPool<DatabaseDbContext>(options =>
        //    {
        //        options.UseSqlServer(configuration.GetConnectionString("ItemDbConnection"));
        //    });
        //}
    }
}
