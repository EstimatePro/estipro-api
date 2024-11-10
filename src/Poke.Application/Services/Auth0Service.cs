using Microsoft.Extensions.Options;
using Poke.Application.Abstractions.Clients;
using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Poke.Application.Options;
using Poke.Application.Services.Interfaces;

namespace Poke.Application.Services;

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
