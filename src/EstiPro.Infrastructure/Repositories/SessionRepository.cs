using Microsoft.EntityFrameworkCore;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Repositories;

public sealed class SessionRepository(ApplicationDbContext dbContext) : ISessionRepository
{
    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Sessions.SingleOrDefaultAsync(session => session.Id == id, cancellationToken);
    }

    public void Add(Session session)
    {
        dbContext.Sessions.Add(session);
    }

    public void Remove(Session session)
    {
        dbContext.Sessions.Remove(session);
    }

    public void Update(Session session)
    {
        dbContext.Sessions.Update(session);
    }
}