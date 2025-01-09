using Auth0.ManagementApi.Models;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.Services.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace EstiPro.Application.Services;

public class Auth0UserService(Auth0ManagementClientService auth0ManagementClient, ILogger<Auth0UserService> logger)
    : IAuth0UserService
{
    public async Task<Result<User>> CreateUserAsync(
        UserRegistrationDataDto userRegistrationData,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Attempting to create a new Auth0 user with email: {Email}", userRegistrationData.Email);

        try
        {
            var client = await auth0ManagementClient.GetClientAsync();
            var userRequest = new UserCreateRequest
            {
                Email = userRegistrationData.Email,
                Password = userRegistrationData.Password,
                NickName = userRegistrationData.Nickname,
                UserId = userRegistrationData.UserMetadata["user_guid"],
                Connection = "Username-Password-Authentication",
                UserMetadata = userRegistrationData.UserMetadata
            };

            var newUser = await client.Users.CreateAsync(userRequest, cancellationToken);

            if (newUser != null)
            {
                logger.LogInformation("Successfully created Auth0 user with email: {Email}, ID: {UserId}",
                    userRegistrationData.Email,
                    newUser.UserId);
                return Result.Ok(newUser);
            }

            logger.LogWarning("Failed to create Auth0 user with email: {Email}. Response was null.",
                userRegistrationData.Email);
            return Result.Fail("UserCreationFailed: Response was null.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a new Auth0 user with email: {Email}",
                userRegistrationData.Email);
            return Result.Fail($"UserCreationFailed: {ex.Message}");
        }
    }
}