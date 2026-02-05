using BookingService.Housing.Models;
using BookingService.Shared;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository
{
    Task<List<Property>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest);
    Task<Property?> GetPropertyAsync(int propertyId);
    Task CreatePropertyAsync(Property property);
    Task UpdatePropertyAsync(Property property);
    Task DeletePropertyAsync(int id);
}