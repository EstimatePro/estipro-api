using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRoomCommand, Result>
{
    public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        if (room.CreatedByUserId != command.UserId)
        {
            return Result.Fail("Forbidden");
        }

        roomRepository.Remove(room);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
