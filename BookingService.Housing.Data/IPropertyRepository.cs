using BookingService.Housing.Models;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task UpdateNameAsync(int id, string name);
    Task UpdateDescriptionAsync(int id, string description);
    Task UpdateTagsAsync(int id, List<int> tags);
}