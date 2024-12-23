using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using Refit;

namespace EstiPro.Application.Abstractions.Clients;

public interface IAuth0Client
{
    [Post("/dbconnections/signup")]
    Task<UserDto?> Signup([Body] UserRegistrationDataDto request);

    [Post("/oauth/token")]
    Task<AccessTokenResponse> GetAccessToken(AccessTokenRequest tokenRequest);
}
