using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Rooms;

namespace Poke.Application.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(
    Guid UserId,
    CreateRoomDto Room) : ICommand<Result<RoomDto>>;
