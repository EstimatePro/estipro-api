using Microsoft.Extensions.Options;
using EstiPro.Application.Abstractions.Clients;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.Options;
using EstiPro.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EstiPro.Application.Services;

public sealed class Auth0Service(IAuth0Client client, IOptions<Auth0Options> options, ILogger<Auth0Service> logger)
    : IAuth0Service
{
    public async Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest)
    {
        logger.LogError("Email is null or empty");
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