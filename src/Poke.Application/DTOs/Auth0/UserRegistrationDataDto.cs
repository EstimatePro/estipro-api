using System.Text.Json.Serialization;

namespace Poke.Application.DTOs.Auth0;

public sealed class UserRegistrationDataDto(Guid id, string email, string password) : Auth0RegisterRequest
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = email;

    [JsonPropertyName("password")]
    public string Password { get; init; } = password;

    [JsonPropertyName("user_metadata")]
    public IDictionary<string, string> UserMetadata { get; set; } = new Dictionary<string, string>
    {
        {
            "user_guid", id.ToString()
        }
    };
}
