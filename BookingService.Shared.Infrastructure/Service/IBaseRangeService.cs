namespace BookingService.Shared.Infrastructure.Service;

public interface IBaseRangeService<TCreateDto, TUpdateDto>
{
    Task AddRangeAsync(int id, List<TCreateDto> createDto);
    Task UpdateRangeAsync(int id, List<TUpdateDto> updateDto);
    Task DeleteRangeAsync(int id);
}