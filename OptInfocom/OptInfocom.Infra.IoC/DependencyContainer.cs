using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptInfocom.Item.Application.Interfaces;
using OptInfocom.Item.Application.Services;
using OptInfocom.Item.Data.Context;
using OptInfocom.Item.Data.Repository;
using OptInfocom.Item.Domain.Interfaces;

namespace OptInfocom.Infra.IoC
{
    public static class DependencyContainer
    {
        //public static void RegisterServices(IServiceCollection services)
        //{
        //    //Domain Bus

        //    //Application Service
        //    services.AddTransient<IItemService, ItemService>();

        //    //Data
        //    services.AddTransient<IItemRepository, ItemRepository>();

        //    services.AddTransient<ItemDbContext>();
        //}

        public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ItemDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ItemDbConnection"),
            //    b => b.MigrationsAssembly(typeof(ItemDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            //services.AddScoped<IItemDbContext>(provider =>
            //provider.GetService<ItemDbContext>());


            services.AddDbContextPool<ItemDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ItemDbConnection"));
            });

            //Application Service
            services.AddTransient<IItemService, ItemService>();

            //Data
            services.AddTransient<IItemRepository, ItemRepository>();

            return services;
        }
    }
}
