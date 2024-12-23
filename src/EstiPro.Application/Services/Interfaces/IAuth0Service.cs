using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;

namespace EstiPro.Application.Services.Interfaces;

public interface IAuth0Service
{
    Task<UserDto?> RegisterUser(UserRegistrationDataDto userData);
    Task<AccessTokenResponse> GetAccessToken(AccessTokenUserCredentials tokenRequest);
}
