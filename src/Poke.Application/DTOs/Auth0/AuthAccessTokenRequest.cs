using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Poke.Application.DTOs.Auth0;

public abstract class Auth0RegisterRequest
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("connection")]
    public string Connection { get; set; }
}
