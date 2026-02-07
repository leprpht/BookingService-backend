using BookingService.Housing.Models;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public interface IUnitRepository : IBaseRepository<Unit>
{
    Task<Unit?> GetUnitAsync(int unitId);
}