using FluentResults;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.DTOs.VotingItems;

namespace EstiPro.Application.Rooms.Commands.AddVote;

public record AddVoteCommand(Guid RoomId, Guid TicketId, Guid CreatedByUserId, AddVoteDto Vote) : ICommand<Result<VoteDto>>;
