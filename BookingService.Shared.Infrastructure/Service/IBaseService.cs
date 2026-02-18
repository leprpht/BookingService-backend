namespace BookingService.Shared.Infrastructure.Service;

public interface IBaseService<TModel, in TCreateDto, in TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<TModel?> GetByIdAsync(int id);
    Task CreateAsync(int userId, TCreateDto createDto);
    Task UpdateAsync(int userId, TUpdateDto updateDto);
    Task DeleteAsync(int id, int userId);
}