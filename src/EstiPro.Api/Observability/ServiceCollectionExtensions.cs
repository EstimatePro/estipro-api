using Grafana.OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace EstiPro.Api.Observability;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomObservability(this IServiceCollection service,
        WebApplicationBuilder builder)
    {
        var endpoint = builder.Configuration["OpenTelemetry:Exporter.Endpoint"];
        var authHeader = builder.Configuration["OpenTelemetry:Exporter.Headers.Authorization"];

        var serviceName =
            string.Join('-', builder.Environment.ApplicationName.Split('.').Select(x => x.ToLowerInvariant()));

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddHostDetector()
            .AddContainerDetector()
            .AddService(serviceName: serviceName, serviceVersion: "1.0.0")
            .AddAttributes(new Dictionary<string, object>
            {
                { "deployment.environment", builder.Environment.EnvironmentName },
                { "organization", "EstiPro" }
            });

        var otlpExporter = new OtlpExporter
        {
            Protocol = OtlpExportProtocol.HttpProtobuf,
            Endpoint = new Uri(endpoint!),
            Headers = authHeader
        };

        builder.Logging.ClearProviders();

        builder.Services.AddOpenTelemetry()
            .WithTracing(configure =>
            {
                configure
                    .SetResourceBuilder(resourceBuilder)
                    .UseGrafana(config => { config.ExporterSettings = otlpExporter; })
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddOtlpExporter()
                    .AddConsoleExporter();
            })
            .WithMetrics(configure =>
            {
                configure
                    .SetResourceBuilder(resourceBuilder)
                    .UseGrafana(config => { config.ExporterSettings = otlpExporter; })
                    .AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter()
                    .AddConsoleExporter();
            });

        builder.Logging.AddOpenTelemetry(configure =>
        {
            configure
                .SetResourceBuilder(resourceBuilder)
                .UseGrafana(config => { config.ExporterSettings = otlpExporter; })
                .AddOtlpExporter();

            configure.IncludeScopes = true;
            configure.IncludeFormattedMessage = true;
            configure.ParseStateValues = true;
        });

        return service;
    }
}