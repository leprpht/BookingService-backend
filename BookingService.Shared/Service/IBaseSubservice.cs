namespace BookingService.Shared.Service;

public interface IBaseSubservice<TCreateDto, TUpdateDto>
{
    Task AddRangeAsync(int id, List<TCreateDto> createDto);
    Task UpdateRangeAsync(int id, List<TUpdateDto> updateDto);
    Task DeleteRangeAsync(int id);
}