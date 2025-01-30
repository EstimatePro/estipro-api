using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Application.Common;
using EstiPro.Application.DTOs.VotingItems;
using EstiPro.Application.Mappers;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Rooms.Commands.AddVote;

public sealed class AddVoteCommandHandler(
    IRoomRepository roomRepository,
    ITicketRepository ticketRepository,
    IVoteRepository voteRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddVoteCommand, Result<VoteDto>>
{
    public async Task<Result<VoteDto>> Handle(AddVoteCommand command, CancellationToken cancellationToken)
    {
        var room = await roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

        if (room == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Room>(command.RoomId.ToString()));
        }

        var ticket = await ticketRepository.GetByIdAsync(command.TicketId, cancellationToken);

        if (ticket == null)
        {
            return Result.Fail(CommonErrors.EntityNotFoundError<Ticket>( command.TicketId.ToString()));
        }

        if (room.Sessions == null || !room.Sessions.Select(s => s.UserId).Contains(command.CreatedByUserId))
        {
            return Result.Fail($"User with {command.CreatedByUserId} has no access to room!");
        }

        var newVote = new Vote(
            Guid.CreateVersion7(),
            ticket.Id,
            command.CreatedByUserId,
            command.Vote.Mark);

        voteRepository.Add(newVote);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(VoteMapper.VoteToVoteDto(newVote));
    }
}
