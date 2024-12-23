using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Mappers;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Users.Queries;

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