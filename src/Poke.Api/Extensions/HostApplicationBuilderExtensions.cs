using Poke.Api.Vault;

namespace Poke.Api.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static IServiceCollection AddVaultConfiguration(this IHostApplicationBuilder builder)
    {
        builder
            .Configuration
            .Sources
            .Add(new VaultConfigurationSource(builder));

        return builder.Services;
    }
}
