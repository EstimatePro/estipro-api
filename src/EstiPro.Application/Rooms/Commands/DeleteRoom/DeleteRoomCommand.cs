using FluentResults;
using EstiPro.Application.Abstractions.Messaging;

namespace EstiPro.Application.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
