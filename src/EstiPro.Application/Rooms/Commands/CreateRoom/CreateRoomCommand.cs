using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Rooms;

namespace EstiPro.Application.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(
    Guid UserId,
    CreateRoomDto Room) : ICommand<Result<RoomDto>>;
