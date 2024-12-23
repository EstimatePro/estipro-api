using Microsoft.Extensions.Options;
using EstiPro.Application.Abstractions.Clients;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Options;
using EstiPro.Application.Services.Interfaces;

namespace EstiPro.Application.Services;

public sealed class Auth0Service(IAuth0Client client, IOptions<Auth0Options> options) : IAuth0Service
{
    public async Task<UserDto?> RegisterUser(UserRegistrationDataDto userData)
    {
        userData.ClientId = options.Value.ClientId;
        userData.Connection = options.Value.Connection;
        return await client.Signup(userData);
    }

    public async Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest)
    {
        ArgumentException.ThrowIfNullOrEmpty(tokenRequest.Email);
        var request = new AccessTokenRequest
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
