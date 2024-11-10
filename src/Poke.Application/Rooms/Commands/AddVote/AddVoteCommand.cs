using FluentResults;
using Poke.Application.Abstractions.Messaging;
using Poke.Application.DTOs.VotingItems;

namespace Poke.Application.Rooms.Commands.AddVote;

public record AddVoteCommand(Guid RoomId, Guid TicketId, Guid CreatedByUserId, AddVoteDto Vote) : ICommand<Result<VoteDto>>;
