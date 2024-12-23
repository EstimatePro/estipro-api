using Microsoft.EntityFrameworkCore;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Repositories;

public sealed class TicketRepository(ApplicationDbContext dbContext) : ITicketRepository
{
    public void Add(Ticket ticket)
    {
        dbContext.Tickets.Add(ticket);
    }

    public void Remove(Ticket ticket)
    {
        dbContext.Tickets.Remove(ticket);
    }

    public void Update(Ticket ticket)
    {
        dbContext.Tickets.Update(ticket);
    }

    public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Tickets.SingleOrDefaultAsync(room => room.Id == id, cancellationToken);
    }
}