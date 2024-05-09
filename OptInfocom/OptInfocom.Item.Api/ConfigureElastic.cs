using Nest;

namespace OptInfocom.Item.Api
{
    public static class ConfigureElastic
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["ElasticSettings:BaseUrl"];
            var index = configuration["ElasticSettings:DefaultIndex"];
            var userName = configuration["ElasticSettings:UserName"];
            var password = configuration["ElasticSettings:Password"];
            var certificate = configuration["ElasticSettings:Certificate"];

            var settings = new ConnectionSettings(new Uri(baseUrl ?? ""))
                .PrettyJson()
                .CertificateFingerprint(certificate)
                .BasicAuthentication(userName, password)
                .DefaultIndex(index);
            settings.EnableApiVersioningHeader();
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}
