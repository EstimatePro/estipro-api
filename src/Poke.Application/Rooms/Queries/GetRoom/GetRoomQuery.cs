using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Rooms;

namespace Poke.Application.Rooms.Queries.GetRoom;

public sealed record GetRoomQuery(Guid Id, Guid UserId) : IQuery<Result<RoomDto>>;
