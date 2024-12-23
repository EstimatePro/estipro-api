using System.Text.Json;
using System.Text.Json.Nodes;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace EstiPro.Api.Vault;

public sealed class VaultConfigurationProvider(IHostApplicationBuilder hostBuilder) : ConfigurationProvider
{
    private const string VaultUrl = "VAULT_API_URL";
    private const string VaultToken = "VAULT_API_TOKEN";

    public override void Load()
    {
        if (hostBuilder.Environment.IsEnvironment("Tests"))
        {
            return;
        }

        CustomLoad();
    }

    private void CustomLoad()
    {
        var vaultUrl = GetEnvironmentVariableOrThrow(VaultUrl);
        var vaultToken = GetEnvironmentVariableOrThrow(VaultToken);

        var vaultClientSettings = new VaultClientSettings(vaultUrl, new TokenAuthMethodInfo(vaultToken));
        var vaultClient = new VaultClient(vaultClientSettings);

        var mountPoint = hostBuilder.Configuration["Vault:RootFolder"];
        var path =
            $"{hostBuilder.Environment.EnvironmentName.ToLowerInvariant()}/{hostBuilder.Environment.ApplicationName}";

        var allKeyValues = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync<JsonObject>(path, mountPoint: mountPoint)
            .Result.Data.Data;

        foreach (var keyValue in allKeyValues)
        {
            foreach (KeyValuePair<string, string> pair in keyValue.Value.Deserialize<IDictionary<string, string>>()!)
            {
                var compositeKey = $"{keyValue.Key}:{pair.Key}";
                Data.TryAdd(compositeKey, pair.Value);
            }
        }
    }

    private string GetEnvironmentVariableOrThrow(string variableName)
    {
        var normalizedKey = NormalizeKey(variableName);
        var value = hostBuilder.Configuration[$"Vault:{variableName}"] ??
                    Environment.GetEnvironmentVariable(normalizedKey);
        ArgumentException.ThrowIfNullOrEmpty($"{variableName} environment variable not found");

        return value!;
    }

    private static string NormalizeKey(string key)
    {
        return key.Replace("__", ConfigurationPath.KeyDelimiter);
    }
}