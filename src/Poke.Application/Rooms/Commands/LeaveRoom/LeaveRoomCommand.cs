using FluentResults;
using Poke.Application.Abstractions.Messaging;

namespace Poke.Application.Rooms.Commands.LeaveRoom;

public record LeaveRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
