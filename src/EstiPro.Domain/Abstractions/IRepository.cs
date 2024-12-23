namespace EstiPro.Domain.Abstractions;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}