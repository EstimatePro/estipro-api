using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Rooms;

namespace EstiPro.Application.Rooms.Queries.GetRoom;

public sealed record GetRoomQuery(Guid Id, Guid UserId) : IQuery<Result<RoomDto>>;
