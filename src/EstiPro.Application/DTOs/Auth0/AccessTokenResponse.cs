using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EstiPro.Application.DTOs.Auth0;

public sealed class AccessTokenResponse
{
    [JsonPropertyName("token_type")]

    public string TokenType { get; init; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; init; }
}
