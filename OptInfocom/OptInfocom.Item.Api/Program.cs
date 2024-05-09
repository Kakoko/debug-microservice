using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.ResponseCompression;
using OptInfocom.Item.Api;
using OptInfocom.Infra.IoC;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//ConfigureAllServices.ConfigureSupervisor(builder.Services);
//ConfigureAllServices.AddConnectionProvider(builder.Services, builder.Configuration);

builder.Services.ImplementPersistence(builder.Configuration);

//https://github.com/dotnet/aspnet-api-versioning/wiki/Swashbuckle-Integration
builder.Services.AddApiVersioning();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0); //DefaultApiVersion is used to set the default version to API
    config.AssumeDefaultVersionWhenUnspecified = true; //This flag AssumeDefaultVersionWhenUnspecified flag is used to set the default version when the client has not specified any versions
    config.ReportApiVersions = true; //To return the API version in response header.
});
builder.Services.AddApiVersioning().AddMvc().AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Item Microservice", Version = "v1.0" });
    c.SwaggerDoc("v2.0", new OpenApiInfo { Title = "Item Microservice", Version = "v2.0" });

    c.DocInclusionPredicate((version, desc) =>
    {
        if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
            return false;

        var versions = methodInfo.DeclaringType
        .GetCustomAttributes(true)
        .OfType<ApiVersionAttribute>()
        .SelectMany(attr => attr.Versions);

        var maps = methodInfo
        .GetCustomAttributes(true)
        .OfType<MapToApiVersionAttribute>()
        .SelectMany(attr => attr.Versions)
        .ToArray();

        return versions.Any(v => $"v{v.ToString()}" == version)
        && (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));
    });
});

//By Sambhav :: Cross Domain Authentication To Access API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("*") // Replace with the allowed domain(s)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.ApiName = "apiname";
        options.Authority = "mytokenurl";
        options.RequireHttpsMetadata = false; //Commented By Sambhav :: Gets or sets if HTTPS is required for the metadata address or authority. The default is true. This should be disabled only in development environments.
    });

#region(Added By Sambhav For Compression ===============================================)
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
          options.Level = System.IO.Compression.CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});
#endregion
builder.Services.AddElasticSearch(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", $"Item Microservice v1.0");
        c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", $"Item Microservice v2.0");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", $"Item Microservice v1.0");
        c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", $"Item Microservice v2.0");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseResponseCompression();//Added By Sambhav For Compression
app.Run();
