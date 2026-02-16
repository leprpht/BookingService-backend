using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IUnitService : IBaseService<UnitCreationDto, UnitUpdateDto>
{
    Task UpdateNameAsync(int id, string name);
}