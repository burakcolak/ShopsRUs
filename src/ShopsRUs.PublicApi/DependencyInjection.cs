using Microsoft.OpenApi.Models;
using ShopsRUs.PublicApi.Common.Mapping;

namespace ShopsRUs.PublicApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        AddSwagger(services);

        services.AddMappings();
        return services;

    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopsRUs Public API", Version = "v1" });
        });
        services.AddSwaggerGen();
    }
}
