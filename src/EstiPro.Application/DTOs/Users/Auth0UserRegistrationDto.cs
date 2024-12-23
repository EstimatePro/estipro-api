namespace EstiPro.Application.DTOs.Users;


public class Auth0UserRegistrationDto
{
    public required Guid UserId { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}