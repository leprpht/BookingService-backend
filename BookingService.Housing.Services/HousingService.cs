using BookingService.Housing.DTOs;
using BookingService.Shared;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class HousingService(IHousingRepository repository) : IHousingService
{
    public async Task<List<PropertyPageDto>> GetAvailablePropertiesAsync(FilterOptions filterOptions, PageRequest pageRequest)
    {
        var properties = await repository.GetAvailablePropertiesAsync(filterOptions, pageRequest);
        return properties.Select(p => p.ToPropertyPageDto(filterOptions.Period)).ToList();
    }

    public async Task<PropertyListingDto?> GetPropertyDetailsAsync(int propertyId, PeriodRequest periodRequest)
    {
        var property = await repository.GetPropertyAsync(propertyId, periodRequest);
        return property?.ToPropertyListingDto(periodRequest);
    }

    public async Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest)
    {
        var unit = await repository.GetUnitAsync(unitId, periodRequest);
        return unit?.ToUnitDto(periodRequest);
    }
}