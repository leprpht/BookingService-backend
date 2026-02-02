using BookingService.Housing.DTOs;
using BookingService.Housing.DTOs.Property;
using BookingService.Shared;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;

namespace BookingService.Housing.Services;

public interface IPropertyService
{
    Task<List<PropertyPageDto>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest);
    Task<PropertyListingDto?> GetPropertyDetailsAsync(int propertyId, PeriodRequest periodRequest);
    Task CreatePropertyAsync(PropertyCreationDto createPropertyDto);
    Task UpdatePropertyAsync(PropertyUpdateDto propertyUpdateDto);
    Task DeletePropertyAsync(int id);
}