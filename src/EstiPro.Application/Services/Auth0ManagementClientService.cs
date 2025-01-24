using Auth0.ManagementApi;
using EstiPro.Application.Abstractions.Clients;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EstiPro.Application.Services;

public class Auth0ManagementClientService(
    IAuth0Client auth0Client,
    IOptions<Auth0Options> options,
    ILogger<Auth0ManagementClientService> logger)
{
    private readonly Auth0Options _auth0Options = options.Value;
    private ManagementApiClient? _managementApiClient;
    private string? _accessToken;
    private DateTime _tokenExpiration;
    private readonly SemaphoreSlim _clientLock = new(1, 1);

    public async Task<ManagementApiClient> GetClientAsync()
    {
        await _clientLock.WaitAsync();
        try
        {
            if (_managementApiClient == null || TokenExpired())
            {
                logger.LogInformation("Creating or refreshing the Auth0 ManagementApiClient...");
                await RefreshClientAsync();
                logger.LogInformation("Auth0 ManagementApiClient is ready.");
            }

            return _managementApiClient!;
        }
        finally
        {
            _clientLock.Release();
        }
    }

    private bool TokenExpired()
    {
        // Consider a small buffer to handle network delays or clock skew.
        return string.IsNullOrEmpty(_accessToken) || _tokenExpiration <= DateTime.UtcNow.AddMinutes(-1);
    }

    private async Task RefreshClientAsync()
    {
        _accessToken = await GetAccessTokenAsync();
        _managementApiClient = new ManagementApiClient(
            token: _accessToken,
            domain: _auth0Options.PureDomain
        );
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        logger.LogInformation("Requesting new Auth0 access token...");
        var request = new AccessTokenRequest
        {
            GrantType = _auth0Options.EstiProApi.GrantType,
            ClientId = _auth0Options.EstiProApi.ClientId,
            ClientSecret = _auth0Options.EstiProApi.ClientSecret,
            Audience = _auth0Options.EstiProApi.Audience
        };

        try
        {
            var response = await auth0Client.GetAccessToken(request);
            logger.LogInformation("Access token retrieved successfully.");

            // Assume token expiration is provided in the response.
            _tokenExpiration = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            return response.AccessToken;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve Auth0 access token.");
            throw new InvalidOperationException("Failed to retrieve Auth0 access token.", ex);
        }
    }
}