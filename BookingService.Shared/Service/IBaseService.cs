namespace BookingService.Shared.Service;

public interface IBaseService<in TCreateDto, in TUpdateDto>
    where TCreateDto : class
    where TUpdateDto : class
{
    Task CreateAsync(TCreateDto createDto);
    Task UpdateAsync(TUpdateDto updateDto);
    Task DeleteAsync(int id);
}