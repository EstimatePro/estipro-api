using EstiPro.Domain.Entities;

namespace EstiPro.Domain.Abstractions;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
