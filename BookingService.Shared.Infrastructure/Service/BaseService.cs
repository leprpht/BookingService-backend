using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Shared.Infrastructure.Service;

public abstract class BaseService<TModel, TCreateDto, TUpdateDto>(IBaseRepository<TModel> repository)
    : IBaseService<TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(TCreateDto dto);
    protected abstract TModel MapUpdate(TUpdateDto dto);
    
    public async Task CreateAsync(TCreateDto createDto)
    {
        var model = MapCreate(createDto);
        await repository.AddAsync(model);
    }

    public async Task UpdateAsync(TUpdateDto updateDto)
    {
        var model = MapUpdate(updateDto);
        await repository.UpdateAsync(model);
    }

    public async Task DeleteAsync(int id)
    { 
        await repository.DeleteAsync(id);
    }
} 