using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Shared.Infrastructure.Service;

public abstract class BaseRangeService<TModel, TCreateDto, TUpdateDto>(IBaseRangeRepository<TModel> repository)
    : IBaseRangeService<TCreateDto, TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected abstract TModel MapCreate(Guid id, TCreateDto dto);
    protected abstract TModel MapUpdate(Guid id, TUpdateDto dto);
    
    public async Task AddRangeAsync(Guid id, Guid userId, List<TCreateDto> createDto)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        var modelList = createDto
            .Select(dto => MapCreate(id, dto))
            .ToList();
        
        await repository.AddRangeAsync(modelList);
    }

    public async Task UpdateRangeAsync(Guid id, Guid userId, List<TUpdateDto> updateDto)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        var modelList = updateDto
            .Select(dto => MapUpdate(id, dto))
            .ToList();
        
        await repository.UpdateRangeAsync(modelList);
    }
    
    public async Task DeleteRangeAsync(Guid id, Guid userId)
    {
        var parent = await repository.GetByOwnerIdAsync(userId);
        if (parent.GetType().GetProperty("OwnerId")!.GetValue(parent)!.Equals(id))
            throw new ForbidException("Parent owner ID does not match the user's ID.");
        
        await repository.DeleteRangeAsync(id);
    }
}