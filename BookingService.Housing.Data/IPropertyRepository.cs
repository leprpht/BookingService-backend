using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task UpdateNameAsync(int id, string name);
    Task UpdateDescriptionAsync(int id, string description);
    Task UpdateTagsAsync(int id, List<int> tags);
}