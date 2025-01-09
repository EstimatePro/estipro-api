using Microsoft.Extensions.Options;
using EstiPro.Application.Abstractions.Clients;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Options;
using EstiPro.Application.Services.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace EstiPro.Application.Services;

public sealed class Auth0Service(IAuth0Client client, IOptions<Auth0Options> options, ILogger<Auth0Service> log)
    : IAuth0Service
{
    public async Task<Result<UserDto?>> RegisterUser(UserRegistrationDataDto userData,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        //try
        //{
            // var userCreateRequest = new UserCreateRequest
            // {
            //     Connection = options.Value.Connection,
            //     Email = userData.Email,
            //     Password = userData.Password,
            //     VerifyEmail = true
            // };
            //
            // var managementApiClient = new ManagementApiClient(
            //     token: options.Value.Domain,
            //     domain: options.Value.Domain
            // );
            //
            // try
            // {
            //     var result = await managementApiClient.Users.CreateAsync(userCreateRequest, cancellationToken);
            //     return result == null
            //         ? Result.Fail("Null result")
            //         : Result.Ok(new UserDto(Guid.Parse(result.UserId), result.Email, result.UserName));
            // }
            // catch (ApiException ex)
            // {
            //     return Result.Fail($"Auth0 API Error: {ex.Message}");
            // }
            // catch (Exception ex)
            // {
            //     return Result.Fail($"An error occurred: {ex.Message}");
            // }
            
        //}
    }

    public async Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest)
    {
        ArgumentException.ThrowIfNullOrEmpty(tokenRequest.Email);
        var request = new AccessTokenUserCredentials
        {
            UserName = tokenRequest.Email,
            Password = tokenRequest.Password,
            GrantType = "password",
            ClientId = options.Value.ClientId,
            ClientSecret = options.Value.ClientSecret,
            Audience = options.Value.Audience,
        };
        return await client.GetAccessToken(request);
    }
}