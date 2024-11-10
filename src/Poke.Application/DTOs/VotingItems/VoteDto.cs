namespace Poke.Application.DTOs.VotingItems;

public record VoteDto(Guid Id, Guid UserId, Guid TicketId, int Mark, DateTimeOffset CreatedDateTimeOffset);
