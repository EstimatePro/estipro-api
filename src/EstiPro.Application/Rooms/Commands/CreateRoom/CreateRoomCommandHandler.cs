using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Application.DTOs.Rooms;
using EstiPro.Application.Mappers;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;
using EstiPro.Domain.Enums;

namespace EstiPro.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler(
    IRoomRepository roomRepository,
    ISessionRepository sessionRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateRoomCommand, Result<RoomDto>>
{
    public async Task<Result<RoomDto>> Handle(
        CreateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<User>(command.UserId.ToString()));
        }

        var newRoom = new Room(Guid.NewGuid(), command.Room.Name, command.UserId);
        roomRepository.Add(newRoom);

        var session = new Session(Guid.NewGuid(), newRoom.Id, user.Id, Role.Owner);
        sessionRepository.Add(session);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(RoomMapper.RoomToRoomDto(newRoom));
    }
}