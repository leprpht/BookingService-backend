namespace BookingService.Shared.Infrastructure.Repository;

public interface IBaseRangeRepository<TModel>
    where TModel : class
{
    Task AddRangeAsync(List<TModel> entity);
    Task UpdateRangeAsync(List<TModel> entity);
    Task DeleteRangeAsync(Guid id);
}