using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Poke.Application.Abstractions.Clients;
using Poke.Application.Options;
using Poke.Application.Services;
using Poke.Application.Services.Interfaces;
using Refit;

namespace Poke.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        // Configure
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));

        // Add clients
        services.AddRefitClient<IAuth0Client>()
            .ConfigureHttpClient(
                (sp, client) =>
                {
                    var options = sp.GetRequiredService<IOptions<Auth0Options>>().Value;
                    client.BaseAddress = new Uri(options.Domain);
                });

        // Add services
        services.AddTransient<IAuth0Service, Auth0Service>();

        return services;
    }
}
