namespace BookingService.Shared.Repository;

public interface IBaseSubrepository<T> where T : class
{
    Task<List<T>> GetAllAsync(int id);
    Task AddRangeAsync(IEnumerable<T> entity);
    Task UpdateRangeAsync(IEnumerable<T> entity);
    Task DeleteRangeAsync(int id);
}