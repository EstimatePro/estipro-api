using Poke.Application.DTOs.Tickets;
using Poke.Domain.Entities;

namespace Poke.Application.Mappers;

public static class TicketMapper
{
    public static TicketDto TicketToTicketDto(Ticket ticket)
    {
        return new TicketDto(ticket.Id, ticket.Name, ticket.Description, ticket.Votes?.Select(VoteMapper.VoteToVoteDto).ToArray());
    }
}
