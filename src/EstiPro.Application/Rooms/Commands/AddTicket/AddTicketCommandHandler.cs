using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Application.DTOs.Tickets;
using EstiPro.Application.Mappers;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;
using EstiPro.Domain.Enums;

namespace EstiPro.Application.Rooms.Commands.AddTicket;

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
