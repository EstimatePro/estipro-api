using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Poke.Domain.Entities;

namespace Poke.Application.Mappers;

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
