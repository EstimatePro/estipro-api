using Swashbuckle.AspNetCore.SwaggerGen;

namespace EstiPro.Api.Swagger;

public static class SwaggerExtensions
{
    public static SwaggerGenOptions SetupSwaggerDoc(
        this SwaggerGenOptions options,
        IWebHostEnvironment environment)
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = $"EstiPro API. {environment.EnvironmentName}",
            Version = "v1",
            Description = $"Estimate like a Pro. {environment.EnvironmentName}"
        });
        return options;
    }
}