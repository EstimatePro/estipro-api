using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Users;

namespace Poke.Application.Users.Queries;

public sealed record GetUserQuery(Guid id) : IQuery<Result<UserDto>>;
