using BookingService.Housing.Models;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task<List<Property>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest);
    Task<Property?> GetPropertyAsync(int propertyId);
    Task UpdateNameAsync(int id, string name);
}