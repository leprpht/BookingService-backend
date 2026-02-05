namespace BookingService.Shared.Repository;

public interface IRepository<in T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}