using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Application.Rooms.Commands.LeaveRoom;

public class LeaveRoomCommandHandler(
    IRoomRepository roomRepository,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<LeaveRoomCommand, Result>
{
    public async Task<Result> Handle(LeaveRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);
        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        if (room.Sessions != null && room.Sessions.All(s => s.UserId != command.UserId))
        {
            return Result.Fail($"User: {command.UserId} already leaved!");
        }

        sessionRepository.Remove(room.Sessions!.Single(s => s.UserId == command.UserId));
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
