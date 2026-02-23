using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IUnitService : IBaseService<Unit, UnitCreationDto, UnitUpdateDto>
{
    Task ToggleAllUnitsActiveStatusAsync(Guid propertyId, Guid userId);
    Task ToggleActiveStatusAsync(Guid id, Guid userId);
    Task UpdateNameAsync(Guid id, Guid userId, string name);
}