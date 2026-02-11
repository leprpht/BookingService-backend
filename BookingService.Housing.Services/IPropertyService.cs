using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IPropertyService : IBaseService<PropertyCreationDto, PropertyUpdateDto>
{
    Task<List<PropertyPageDto>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest);
    Task<PropertyListingDto?> GetPropertyDetailsAsync(int propertyId, PeriodRequest periodRequest);
    Task UpdateNameAsync(int id, string name);
    Task UpdateDescriptionAsync(int id, string description);
    Task UpdateTagsAsync(int id, List<int> tags);
}