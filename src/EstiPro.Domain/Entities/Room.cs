using EstiPro.Domain.Primitives;

namespace EstiPro.Domain.Entities;

public sealed class Room(
    Guid id,
    string name,
    Guid createdByUserId)
    : Entity(id)
{
    public string Name { get; private set; } = name;

    public Guid CreatedByUserId { get; private set; } = createdByUserId;

    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public User CreatedByUser { get; set; } = null!;

    public ICollection<Session>? Sessions { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}