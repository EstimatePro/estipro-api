using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.Tickets;

namespace Poke.Application.Rooms.Commands.AddTicket;

public record AddTicketCommand(Guid RoomId, Guid CreatedByUserId, TicketDtoCreate Ticket) : ICommand<Result<TicketDto>>;
