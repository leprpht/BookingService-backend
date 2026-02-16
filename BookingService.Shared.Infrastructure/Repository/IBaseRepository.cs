namespace BookingService.Shared.Infrastructure.Repository;

public interface IBaseRepository<in T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}