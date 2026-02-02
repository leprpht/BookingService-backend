using BookingService.Housing.DTOs;
using BookingService.Housing.DTOs.Property;
using BookingService.Shared;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class PropertyService(IPropertyRepository repository) : IPropertyService
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

    public async Task CreatePropertyAsync(PropertyCreationDto createPropertyDto)
    {
        var property = createPropertyDto.ToProperty();
        await repository.CreatePropertyAsync(property);
    }

    public async Task UpdatePropertyAsync(PropertyUpdateDto propertyUpdateDto)
    {
        var property = propertyUpdateDto.ToProperty();
        await repository.UpdatePropertyAsync(property);
    }

    public async Task DeletePropertyAsync(int id)
    {
        await repository.DeletePropertyAsync(id);
    }
}