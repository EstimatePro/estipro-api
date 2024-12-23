using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.Tickets;

namespace EstiPro.Application.Rooms.Commands.AddTicket;

public record AddTicketCommand(Guid RoomId, Guid CreatedByUserId, TicketDtoCreate Ticket) : ICommand<Result<TicketDto>>;
