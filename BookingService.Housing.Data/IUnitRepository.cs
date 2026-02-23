using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IUnitRepository : IBaseRepository<Unit>
{
    Task ToggleAllUnitsActiveStatusAsync(Guid propertyId, Guid userId);
    Task ToggleActiveStatusAsync(Guid id, Guid userId);
    Task UpdateNameAsync(Guid id, Guid userId, string name);
}