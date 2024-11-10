using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Rooms;

namespace Poke.Application.Rooms.Commands.UpdateRoom;

public sealed record UpdateRoomCommand(
    Guid UserId,
    UpdateRoomDto Room) : ICommand<Result<RoomDto>>;
