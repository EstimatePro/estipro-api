using Serilog;
using Serilog.Core;

namespace Poke.Api.Logs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var levelSwitch = new LoggingLevelSwitch();
        var serviceName =
            string.Join('-', builder.Environment.ApplicationName.Split('.').Select(x => x.ToLowerInvariant()));

        if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Tests")
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Override("Microsoft", levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .Enrich.WithProperty("Service", serviceName)
                .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Override("Microsoft", levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(
                    builder.Configuration["Seq:Url"] !,
                    apiKey: builder.Configuration["Seq:ApiKey"])
                .Enrich.WithProperty("Service", serviceName)
                .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                .CreateLogger();
        }

        builder.Host.UseSerilog();

        return service;
    }
}
