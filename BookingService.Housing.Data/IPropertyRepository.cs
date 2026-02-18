using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task UpdateNameAsync(int id, int ownerId, string name);
    Task UpdateDescriptionAsync(int id, int ownerId, string description);
    Task UpdateTagsAsync(int id, int ownerId, List<int> tags);
}