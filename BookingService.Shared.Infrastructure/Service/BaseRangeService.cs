using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Shared.Infrastructure.Service;

public abstract class BaseRangeService<TModel, TCreateDto, TUpdateDto>(IBaseRangeRepository<TModel> repository)
     : IBaseRangeService<TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(int id, TCreateDto dto);
    protected abstract TModel MapUpdate(int id, TUpdateDto dto);
    
    public async Task AddRangeAsync(int id, int userId, List<TCreateDto> createDto)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        var modelList = createDto
            .Select(dto => MapCreate(id, dto))
            .ToList();
        
        await repository.AddRangeAsync(modelList);
    }

    public async Task UpdateRangeAsync(int id, int userId, List<TUpdateDto> updateDto)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        var modelList = updateDto
            .Select(dto => MapUpdate(id, dto))
            .ToList();
        
        await repository.UpdateRangeAsync(modelList);
    }
    
    public async Task DeleteRangeAsync(int id, int userId)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        await repository.DeleteRangeAsync(id);
    }
}