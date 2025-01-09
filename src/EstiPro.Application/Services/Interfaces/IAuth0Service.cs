using EstiPro.Application.DTOs.Auth0;

namespace EstiPro.Application.Services.Interfaces;

public interface IAuth0Service
{
    Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest);
}
