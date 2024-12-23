using Microsoft.EntityFrameworkCore;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Repositories;

public sealed class VoteRepository(ApplicationDbContext dbContext) : IVoteRepository
{
    public void Add(Vote vote)
    {
        dbContext.Votes.Add(vote);
    }

    public void Remove(Vote vote)
    {
        dbContext.Votes.Add(vote);
    }

    public void Update(Vote vote)
    {
        dbContext.Votes.Update(vote);
    }

    public async Task<Vote?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Votes.SingleOrDefaultAsync(room => room.Id == id, cancellationToken);
    }
}