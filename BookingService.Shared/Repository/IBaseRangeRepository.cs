namespace BookingService.Shared.Repository;

public interface IBaseRangeRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(int id);
    Task AddRangeAsync(List<T> entity);
    Task UpdateRangeAsync(List<T> entity);
    Task DeleteRangeAsync(int id);
}