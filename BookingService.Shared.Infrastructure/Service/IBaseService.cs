namespace BookingService.Shared.Infrastructure.Service;

public interface IBaseService<TModel, in TCreateDto, in TUpdateDto>
    where TModel : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<TModel?> GetByIdAsync(Guid id);
    Task CreateAsync(Guid userId, TCreateDto createDto);
    Task UpdateAsync(Guid userId, TUpdateDto updateDto);
    Task DeleteAsync(Guid id, Guid userId);
}