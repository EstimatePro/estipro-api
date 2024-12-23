using FluentResults;
using EstiPro.Application.Abstractions.Messaging;

namespace EstiPro.Application.Rooms.Commands.LeaveRoom;

public record LeaveRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
