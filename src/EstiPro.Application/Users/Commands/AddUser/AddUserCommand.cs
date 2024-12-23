using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Users;

namespace EstiPro.Application.Users.Commands.AddUser;

public sealed record AddUserCommand(RegisterUserDto User) : ICommand<Result<UserDto>>;
