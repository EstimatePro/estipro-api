using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Refit;

namespace Poke.Application.Abstractions.Clients;

public interface IAuth0Client
{
    [Post("/dbconnections/signup")]
    Task<UserDto?> Signup([Body] UserRegistrationDataDto request);

    [Post("/oauth/token")]
    Task<AccessTokenResponse> GetAccessToken(AccessTokenRequest tokenRequest);
}
