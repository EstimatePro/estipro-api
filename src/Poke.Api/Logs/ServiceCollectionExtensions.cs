using Grafana.OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Poke.Api.Logs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var otelExporterConfig = builder.Configuration.GetSection("OpenTelemetry:Exporter");
        var endpoint = otelExporterConfig.GetValue<string>("Endpoint");
        var headers = otelExporterConfig.GetValue<string>("Headers:Authorization");

        var serviceName =
            string.Join('-', builder.Environment.ApplicationName.Split('.').Select(x => x.ToLowerInvariant()));

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: "1.0.0");

        var otlpExporter = new OtlpExporter
        {
            Protocol = OtlpExportProtocol.HttpProtobuf,
            Endpoint = new Uri(endpoint!),
            Headers = headers
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
                    .AddConsoleExporter();
            });

        builder.Logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(resourceBuilder)
                .UseGrafana(config => { config.ExporterSettings = otlpExporter; })
                .AddConsoleExporter();
        });

        return service;
    }
}