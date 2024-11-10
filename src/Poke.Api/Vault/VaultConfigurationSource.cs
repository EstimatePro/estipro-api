namespace Poke.Api.Vault;

public sealed class VaultConfigurationSource(IHostApplicationBuilder hostBuilder) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider(hostBuilder);
    }
}
