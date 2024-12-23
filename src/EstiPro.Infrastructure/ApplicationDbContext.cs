using Microsoft.EntityFrameworkCore;
using EstiPro.Application.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    DbContext(options),
    IApplicationDbContext,
    IUnitOfWork
{
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Session> Sessions { get; set; }

    public DbSet<Vote> Votes { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
