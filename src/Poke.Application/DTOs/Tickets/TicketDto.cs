using FluentValidation;
using Poke.Application.DTOs.VotingItems;

namespace Poke.Application.DTOs.Tickets;

public record TicketDto(Guid Id, string Title, string? Description, VoteDto[]? Votes) : TicketDtoCreate(Title, Description);

public class TicketDtoValidator : AbstractValidator<TicketDto>
{
    public TicketDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).Length(1, 200);
        When(x => x.Description != null, () => RuleFor(x => x.Description).Length(1, 200));
    }
}
