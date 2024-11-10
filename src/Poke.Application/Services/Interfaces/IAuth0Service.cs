using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;

namespace Poke.Application.Services.Interfaces;

public interface IAuth0Service
{
    Task<UserDto?> RegisterUser(UserRegistrationDataDto userData);
    Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest);
}
