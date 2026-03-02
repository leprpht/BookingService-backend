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
    protected abstract Task<Guid?> GetOwnerIdAsync(Guid parentId); // ← each subclass queries its own parent

    private async Task ValidateOwnershipAsync(Guid parentId, Guid userId)
    {
        var ownerId = await GetOwnerIdAsync(parentId);
        if (ownerId == null || ownerId != userId)
            throw new ForbidException("You do not own this resource.");
    }

    public async Task AddRangeAsync(Guid id, Guid userId, List<TCreateDto> createDto)
    {
        await ValidateOwnershipAsync(id, userId);
        await repository.AddRangeAsync(createDto.Select(dto => MapCreate(id, dto)).ToList());
    }

    public async Task UpdateRangeAsync(Guid id, Guid userId, List<TUpdateDto> updateDto)
    {
        await ValidateOwnershipAsync(id, userId);
        await repository.UpdateRangeAsync(updateDto.Select(dto => MapUpdate(id, dto)).ToList());
    }

    public async Task DeleteRangeAsync(Guid id, Guid userId)
    {
        await ValidateOwnershipAsync(id, userId);
        await repository.DeleteRangeAsync(id);
    }
}