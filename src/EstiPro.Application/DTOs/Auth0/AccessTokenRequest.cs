using System.Text.Json.Serialization;

namespace EstiPro.Application.DTOs.Auth0;

public class AccessTokenRequest
{
    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }

    [JsonPropertyName("audience")]
    public string? Audience { get; set; }
}
