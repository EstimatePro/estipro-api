using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Auth0;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Mappers;
using EstiPro.Application.Services.Interfaces;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Users.Commands.AddUser;

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