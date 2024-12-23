using Microsoft.EntityFrameworkCore;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Room> Rooms { get; set; }

    DbSet<Vote> Votes { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<Ticket> Tickets { get; set; }

    DbSet<Session> Sessions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
