using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Shared.Infrastructure.Service;

public abstract class BaseService<TModel, TCreateDto, TUpdateDto>(IBaseRepository<TModel> repository)
    : IBaseService<TModel, TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(Guid userId, TCreateDto dto);
    protected abstract TModel MapUpdate(Guid userId, TUpdateDto dto);

    public Task<TModel?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id)!;

    public virtual async Task CreateAsync(Guid userId, TCreateDto createDto)
    {
        var model = MapCreate(userId, createDto);
        await repository.AddAsync(model);
    }

    public virtual async Task UpdateAsync(Guid userId, TUpdateDto updateDto)
    {
        var model = MapUpdate(userId, updateDto);
        await repository.UpdateAsync(model);
    }

    public virtual async Task DeleteAsync(Guid id, Guid userId)
    { 
        await repository.GetByOwnerIdAsync(id);
        await repository.DeleteAsync(id, userId);
    }
}