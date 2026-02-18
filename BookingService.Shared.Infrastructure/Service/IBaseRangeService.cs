namespace BookingService.Shared.Infrastructure.Service;

public interface IBaseRangeService<TCreateDto, TUpdateDto>
{
    Task AddRangeAsync(int id, int userId, List<TCreateDto> createDto);
    Task UpdateRangeAsync(int id, int userId, List<TUpdateDto> updateDto);
    Task DeleteRangeAsync(int id, int userId);
}