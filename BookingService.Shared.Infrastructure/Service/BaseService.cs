using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Shared.Infrastructure.Service;

public abstract class BaseService<TModel, TCreateDto, TUpdateDto>(IBaseRepository<TModel> repository)
    : IBaseService<TModel, TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(int userId, TCreateDto dto);
    protected abstract TModel MapUpdate(int userId, TUpdateDto dto);

    public Task<TModel?> GetByIdAsync(int id) =>
        repository.GetByIdAsync(id);

    public virtual async Task CreateAsync(int userId, TCreateDto createDto)
    {
        var model = MapCreate(userId, createDto);
        await repository.AddAsync(model);
    }

    public virtual async Task UpdateAsync(int userId, TUpdateDto updateDto)
    {
        var model = MapUpdate(userId, updateDto);
        await repository.UpdateAsync(model);
    }

    public virtual async Task DeleteAsync(int id, int userId)
    { 
        var parent = await repository.GetByOwnerIdAsync(id);
        await repository.DeleteAsync(id, userId);
    }
} 