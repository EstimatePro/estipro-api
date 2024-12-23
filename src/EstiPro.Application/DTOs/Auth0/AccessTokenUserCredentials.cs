using System.Text.Json.Serialization;


namespace EstiPro.Application.DTOs.Auth0;

public class AccessTokenUserCredentials
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}
