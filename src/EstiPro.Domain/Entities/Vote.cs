using EstiPro.Domain.Primitives;

namespace EstiPro.Domain.Entities;

public sealed class Vote(
    Guid id,
    Guid ticketId,
    Guid userId,
    int mark) : Entity(id)
{
    public Guid UserId { get; private set; } = userId;

    public Guid TicketId { get; private set; } = ticketId;

    public int Mark { get; private set; } = mark;

    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;

    public Ticket Ticket { get; set; } = null!;
}
