using BookingService.Housing.DTOs;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public interface IHousingService
{
    Task<List<PropertyPageDto>> GetAvailablePropertiesAsync(FilterOptions filterOptions, PageRequest pageRequest);
    Task<PropertyListingDto?> GetPropertyDetailsAsync(int propertyId, PeriodRequest periodRequest);
    Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest);
}