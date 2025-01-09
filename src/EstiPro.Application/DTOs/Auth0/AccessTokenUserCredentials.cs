using System.Text.Json.Serialization;


namespace EstiPro.Application.DTOs.Auth0;

public class AccessTokenUserCredentials : AccessTokenRequest
{
    [JsonPropertyName("email")] 
    public string? Email { get; set; }

    [JsonPropertyName("password")] 
    public required string Password { get; set; }

    [JsonPropertyName("username")] 
    public required string UserName { get; set; }
}