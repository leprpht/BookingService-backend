using BookingService.Housing.Models;
using BookingService.Shared;

namespace BookingServices.Housing.Data;

public interface IHousingRepository
{
    Task<List<Property>> GetAvailablePropertiesAsync(FilterOptions filterOptions, PageRequest pageRequest);
    Task<Property?> GetPropertyAsync(int propertyId, PeriodRequest periodRequest);
    Task<Unit?> GetUnitAsync(int unitId, PeriodRequest periodRequest);
}