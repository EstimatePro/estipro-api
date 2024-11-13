using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Users;

namespace Poke.Application.Users.Commands.VerifyUser;

public sealed record CreateUserCommand(Auth0UserRegistrationDto User) : ICommand<Result>;