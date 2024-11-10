using FluentResults;
using Poke.Application.Abstractions.Messaging;

namespace Poke.Application.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
