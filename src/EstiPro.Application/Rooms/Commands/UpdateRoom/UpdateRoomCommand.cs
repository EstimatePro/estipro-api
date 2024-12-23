using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Rooms;

namespace EstiPro.Application.Rooms.Commands.UpdateRoom;

public sealed record UpdateRoomCommand(
    Guid UserId,
    UpdateRoomDto Room) : ICommand<Result<RoomDto>>;
