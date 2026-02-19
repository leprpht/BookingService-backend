namespace BookingService.Shared.Infrastructure.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByOwnerIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id, Guid userId);
}