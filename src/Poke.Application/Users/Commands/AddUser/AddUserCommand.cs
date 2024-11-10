using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Users;

namespace Poke.Application.Users.Commands.AddUser;

public sealed record AddUserCommand(RegisterUserDto User) : ICommand<Result<UserDto>>;
