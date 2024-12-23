using EstiPro.Domain.Enums;
using EstiPro.Domain.Primitives;

namespace EstiPro.Domain.Entities;

public sealed class Session(
    Guid id,
    Guid roomId,
    Guid userId,
    Role userRole) : Entity(id)
{
    public Guid RoomId { get; private set; } = roomId;

    public Guid UserId { get; private set; } = userId;

    public Role UserRole { get; private set; } = userRole;

    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public User User { get; init; } = null!;

    public Room Room { get; init; } = null!;
}