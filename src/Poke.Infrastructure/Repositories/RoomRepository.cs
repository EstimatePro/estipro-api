using Microsoft.EntityFrameworkCore;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Infrastructure.Repositories;

public sealed class RoomRepository(ApplicationDbContext dbContext) : IRoomRepository
{
    public void Add(Room room)
    {
        dbContext.Rooms.Add(room);
    }

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Rooms
            .Include(room => room.Sessions)
            .Include(room => room.Tickets)!
            .ThenInclude(ticket => ticket.Votes)
            .SingleOrDefaultAsync(room => room.Id == id, cancellationToken);
    }

    public void Remove(Room room)
    {
        dbContext.Rooms.Remove(room);
    }

    public void Update(Room room)
    {
        dbContext.Rooms.Update(room);
    }
}