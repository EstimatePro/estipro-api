using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;
using Poke.Domain.Enums;

namespace Poke.Application.Rooms.Commands.JoinRoom;

public class JoinRoomCommandHandler(
    IRoomRepository roomRepository,
    ISessionRepository sessionRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<JoinRoomCommand, Result>
{
    public async Task<Result> Handle(JoinRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        if (room.Sessions != null && room.Sessions.Any(s => s.UserId == command.UserId))
        {
            return Result.Fail($"User: {command.UserId} already joined!");
        }

        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Fail($"User with {command.UserId} was not found!");
        }

        var session = new Session(Guid.NewGuid(), room.Id, user.Id, Role.Common);
        sessionRepository.Add(session);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
