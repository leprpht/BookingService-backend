using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Extensions;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;
using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public class PropertyService(IPropertyRepository repository)
    : BaseService<Property, PropertyCreationDto, PropertyUpdateDto>(repository), IPropertyService
{
    public async Task<List<PropertyPageDto>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest)
    {
        var properties = await repository.GetAvailablePropertiesAsync(housingFilterOptions, pageRequest);
        return properties.Select(p => p.ToPropertyPageDto(housingFilterOptions.Period)).ToList();
    }

    public async Task<PropertyListingDto?> GetPropertyDetailsAsync(int propertyId, PeriodRequest periodRequest)
    {
        var property = await repository.GetPropertyAsync(propertyId);
        return property?.ToPropertyListingDto(periodRequest);
    }
}