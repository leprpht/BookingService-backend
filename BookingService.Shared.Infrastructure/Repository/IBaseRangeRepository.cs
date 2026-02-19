namespace BookingService.Shared.Infrastructure.Repository;

public interface IBaseRangeRepository<TModel>
    where TModel : class
{
    Task<TModel> GetByOwnerIdAsync(Guid id);
    Task AddRangeAsync(List<TModel> entity);
    Task UpdateRangeAsync(List<TModel> entity);
    Task DeleteRangeAsync(Guid id);
}