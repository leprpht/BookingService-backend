using BookingService.Housing.Models;
using BookingService.Shared;

namespace BookingServices.Housing.Data;

public interface IUnitRepository
{
    Task<Unit?> GetUnitAsync(int unitId, PeriodRequest periodRequest);
    Task CreateUnitAsync(Unit unit);
    Task UpdateUnitAsync(Unit unit);
    Task DeleteUnitAsync(int id);
}