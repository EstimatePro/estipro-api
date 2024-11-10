using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Application.DTOs.Rooms;
using Poke.Application.Mappers;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;
using ZiggyCreatures.Caching.Fusion;

namespace Poke.Application.Rooms.Queries.GetRoom;

public class GetRoomQueryHandler(
    IRoomRepository roomRepository,
    IFusionCache cache
) : IQueryHandler<GetRoomQuery, Result<RoomDto>>
{
    public async Task<Result<RoomDto>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var room = await cache.GetOrSetAsync(
            query.Id.ToString(),
            async ct => await roomRepository.GetByIdAsync(query.Id, ct),
            options => options.SetDuration(TimeSpan.FromSeconds(1)), cancellationToken);

        return room == null
            ? Result.Fail(CommonErrors.EntityNotFoundError<Room>(query.Id.ToString()))
            : Result.Ok(RoomMapper.RoomToRoomDto(room));
    }
}