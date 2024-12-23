using FluentResults;
using EstiPro.Application.Abstractions.Messaging;

namespace EstiPro.Application.Rooms.Commands.JoinRoom;

public record JoinRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
