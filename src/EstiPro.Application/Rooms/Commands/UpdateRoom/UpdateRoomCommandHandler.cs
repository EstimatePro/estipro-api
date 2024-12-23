using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Application.DTOs.Rooms;
using EstiPro.Application.Mappers;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler(
    IRoomRepository roomRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateRoomCommand, Result<RoomDto>>
{
    public async Task<Result<RoomDto>> Handle(
        UpdateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<User>(command.UserId.ToString()));
        }

        var room = await roomRepository.GetByIdAsync(command.Room.Id, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.Room.Id.ToString()));
        }

        roomRepository.Update(room);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(RoomMapper.RoomToRoomDto(room));
    }
}