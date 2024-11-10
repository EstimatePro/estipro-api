using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Poke.Application.Mappers;
using Poke.Application.Services.Interfaces;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Application.Users.Commands.AddUser;

public sealed class AddUserCommandHandler(
    IUserRepository userRepository,
    IAuth0Service auth0Service,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.User.Email, cancellationToken);

        if (user != null)
        {
            return Result.Fail($"User with {command.User.Email} already registered.");
        }

        var newUser = new User(Guid.NewGuid(), command.User.Email, command.User.NickName);

        var result = await auth0Service
            .RegisterUser(new UserRegistrationDataDto(newUser.Id, newUser.Email, command.User.Password));

        if (result == null)
        {
            return Result.Fail("User registration fails.");
        }

        userRepository.Add(newUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(UserMapper.UserToUserDto(newUser));
    }
}
