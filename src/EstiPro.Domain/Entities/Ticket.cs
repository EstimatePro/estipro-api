using EstiPro.Domain.Enums;
using EstiPro.Domain.Primitives;

namespace EstiPro.Domain.Entities;

public sealed class Ticket(
    Guid id,
    Guid roomId,
    Guid createdByUserId,
    TicketType type,
    string name,
    string? description = default) : Entity(id)
{
    public Guid RoomId { get; private set; } = roomId;

    public string Name { get; private set; } = name;

    public string? Description { get; private set; } = description;

    public Guid CreatedByUserId { get; private set; } = createdByUserId;

    public TicketType Type { get; private set; } = type;

    public TicketState State { get; private set; } = TicketState.New;

    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public Room Room { get; init; } = null!;

    public ICollection<Vote>? Votes { get; set; } 
}