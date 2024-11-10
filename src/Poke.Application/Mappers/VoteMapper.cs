using Poke.Application.DTOs.VotingItems;
using Poke.Domain.Entities;

namespace Poke.Application.Mappers;

public static class VoteMapper
{
    public static VoteDto VoteToVoteDto(Vote vote)
    {
        return new VoteDto(
            vote.Id,
            vote.UserId,
            vote.TicketId,
            vote.Mark,
            vote.CreatedDate);
    }
}
