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

        // Retrieve all key-values as a JsonObject (JsonNode)
        var allKeyValues = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync<JsonObject>(path, mountPoint: mountPoint)
            .Result.Data.Data;

        foreach (var keyValue in allKeyValues)
        {
            // Call the recursive method to flatten and add nested objects
            FlattenAndAddToData(keyValue.Key, keyValue.Value);
        }
    }

    private void FlattenAndAddToData(string prefix, JsonNode? value)
    {
        if (value is JsonObject jsonObject)
        {
            // If the value is a JsonObject, recursively process its properties
            foreach (var property in jsonObject)
            {
                FlattenAndAddToData($"{prefix}:{property.Key}", property.Value);
            }
        }
        else if (value is JsonValue jsonValue)
        {
            // If the value is a JsonValue (primitive), add it to the dictionary
            if (jsonValue.TryGetValue(out string? stringValue))
            {
                Data.TryAdd(prefix, stringValue);
            }
            else if (jsonValue.TryGetValue(out int intValue))
            {
                Data.TryAdd(prefix, intValue.ToString());
            }
            else if (jsonValue.TryGetValue(out bool boolValue))
            {
                Data.TryAdd(prefix, boolValue.ToString());
            }
            // Handle other primitive types as needed
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