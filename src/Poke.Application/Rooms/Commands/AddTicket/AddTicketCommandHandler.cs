using FluentResults;
using Poke.Application.Abstractions;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.Common;
using Poke.Application.DTOs.Tickets;
using Poke.Application.Mappers;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;
using Poke.Domain.Enums;

namespace Poke.Application.Rooms.Commands.AddTicket;

public sealed class AddTicketCommandHandler(
    IRoomRepository roomRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddTicketCommand, Result<TicketDto>>
{
    public async Task<Result<TicketDto>> Handle(AddTicketCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        var newTicket = new Ticket(
            Guid.NewGuid(),
            room.Id,
            command.CreatedByUserId,
            TicketType.Manual,
            command.Ticket.Title,
            command.Ticket.Description);

        ticketRepository.Add(newTicket);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(TicketMapper.TicketToTicketDto(newTicket));
    }
}
