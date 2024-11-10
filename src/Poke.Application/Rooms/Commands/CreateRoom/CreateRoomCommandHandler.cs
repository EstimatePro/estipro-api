using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Application.DTOs.Rooms;
using Poke.Application.Mappers;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;
using Poke.Domain.Enums;

namespace Poke.Application.Rooms.Commands.CreateRoom;

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