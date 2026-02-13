using BookingService.Shared.Repository;

namespace BookingService.Shared.Service;

public abstract class BaseRangeService<TModel, TCreateDto, TUpdateDto>(IBaseRangeRepository<TModel> repository)
     : IBaseRangeService<TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(int id, TCreateDto dto);
    protected abstract TModel MapUpdate(int id, TUpdateDto dto);
    
    public async Task AddRangeAsync(int id, List<TCreateDto> createDto)
    {
        var modelList = createDto
            .Select(dto => MapCreate(id, dto))
            .ToList();
        
        await repository.AddRangeAsync(modelList);
    }

    public async Task UpdateRangeAsync(int id, List<TUpdateDto> updateDto)
    {
        var modelList = updateDto
            .Select(dto => MapUpdate(id, dto))
            .ToList();
        
        await repository.UpdateRangeAsync(modelList);
    }
    
    public async Task DeleteRangeAsync(int id)
    {
        await repository.DeleteRangeAsync(id);
    }
}