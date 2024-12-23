using EstiPro.Application.DTOs.VotingItems;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Mappers;

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
