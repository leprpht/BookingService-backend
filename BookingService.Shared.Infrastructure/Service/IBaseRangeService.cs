namespace BookingService.Shared.Infrastructure.Service;

public interface IBaseRangeService<TCreateDto, TUpdateDto>
{
    Task AddRangeAsync(Guid id, Guid userId, List<TCreateDto> createDto);
    Task UpdateRangeAsync(Guid id, Guid userId, List<TUpdateDto> updateDto);
    Task DeleteRangeAsync(Guid id, Guid userId);
}