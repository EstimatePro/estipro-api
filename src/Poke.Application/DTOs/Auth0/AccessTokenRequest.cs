using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Poke.Application.DTOs.Auth0;

public sealed class AccessTokenRequest : AccessTokenUserCredentials
{
    [JsonPropertyName("username")]
    public required string UserName { get; set; }
    
    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }

    [JsonPropertyName("audience")]
    public string? Audience { get; set; }
}
