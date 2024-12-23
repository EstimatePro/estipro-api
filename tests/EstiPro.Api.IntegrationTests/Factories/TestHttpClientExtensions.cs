using Microsoft.AspNetCore.Mvc.Testing;

namespace EstiPro.Api.IntegrationTests.Factories;

internal static class TestHttpClientExtensions
{
    internal static readonly WebApplicationFactoryClientOptions? DefaultOptions = new()
    {
        AllowAutoRedirect = true
    };

    internal static HttpClient WithJwtBearer(this HttpClient client, string bearerToken)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        if (string.IsNullOrWhiteSpace(bearerToken))
        {
            throw new ArgumentNullException(nameof(bearerToken));
        }

        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
        return client;
    }
}
