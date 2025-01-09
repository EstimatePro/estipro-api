using EstiPro.Application.DTOs.Auth0;
using Refit;

namespace EstiPro.Application.Abstractions.Clients;

public interface IAuth0Client
{
    [Post("/oauth/token")]
    Task<AccessTokenResponse> GetAccessToken(AccessTokenRequest tokenRequest);
}
