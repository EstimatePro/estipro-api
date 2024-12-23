using EstiPro.Application.DTOs.Tickets;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Mappers;

public static class TicketMapper
{
    public static TicketDto TicketToTicketDto(Ticket ticket)
    {
        return new TicketDto(ticket.Id, ticket.Name, ticket.Description, ticket.Votes?.Select(VoteMapper.VoteToVoteDto).ToArray());
    }
}
