using EstiPro.Domain.Primitives;

namespace EstiPro.Domain.Entities;

public sealed class User(
    Guid id,
    string email,
    string nickName) : Entity(id)
{
    public string NickName { get; private set; } = nickName;

    public string Email { get; private set; } = email;

    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public ICollection<Session>? Sessions { get; set; } 

    public ICollection<Vote>? Votes { get; set; } 

    public ICollection<Room>? RoomsCreated { get; set; } 
}