using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Users;

namespace EstiPro.Application.Users.Queries;

public sealed record GetUserQuery(Guid id) : IQuery<Result<UserDto>>;
