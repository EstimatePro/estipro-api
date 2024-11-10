using Microsoft.EntityFrameworkCore;
using Poke.Domain.Abstractions;
using Poke.Domain.Entities;

namespace Poke.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }

    public void Remove(User user)
    {
        dbContext.Users.Remove(user);
    }

    public void Update(User user)
    {
        dbContext.Users.Remove(user);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
    }
}