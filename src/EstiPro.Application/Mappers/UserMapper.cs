using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Mappers;

public static class UserMapper
{
    public static UserDto UserToUserDto(User user)
    {
        return new UserDto(user.Id, user.Email, user.NickName);
    }

    public static User UserDtoToUser(UserDto user)
    {
        return new User(user.Id, user.Email, user.NickName);
    }

    public static UserRegistrationDataDto UserToRegisterUser(User user, string password)
    {
        return new UserRegistrationDataDto(user.Id, user.Email, password);
    }
}
