using BookingService.Housing.Models;

namespace BookingServices.Housing.Data;

public interface IUnitRepository
{
    Task<Unit?> GetUnitAsync(int unitId);
    Task CreateUnitAsync(Unit unit);
    Task UpdateUnitAsync(Unit unit);
    Task DeleteUnitAsync(int id);
}