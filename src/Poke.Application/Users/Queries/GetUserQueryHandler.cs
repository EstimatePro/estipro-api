using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Application.DTOs.Users;
using Poke.Application.Mappers;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Application.Users.Queries;

public sealed class GetUserQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(query.id, cancellationToken);

        if (user == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<User>(query.id.ToString()));
        }

        return UserMapper.UserToUserDto(user);
    }
}