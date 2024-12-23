using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Users;

namespace EstiPro.Application.Users.Commands.VerifyUser;

public sealed record CreateUserCommand(Auth0UserRegistrationDto User) : ICommand<Result>;