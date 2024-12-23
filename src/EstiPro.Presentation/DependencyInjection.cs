using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

namespace EstiPro.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services
            .AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
            .AddApplicationPart(assembly);

        services.AddFluentValidationAutoValidation();
        services.AddEndpointsApiExplorer();

        return services;
    }
}
