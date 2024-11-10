using FluentResults;
using Poke.Application.Abstractions.Messaging;

namespace Poke.Application.Rooms.Commands.JoinRoom;

public record JoinRoomCommand(Guid RoomId, Guid UserId) : ICommand<Result>;
