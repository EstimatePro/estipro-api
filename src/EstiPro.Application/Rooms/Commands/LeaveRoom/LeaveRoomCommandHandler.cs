using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Rooms.Commands.LeaveRoom;

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
