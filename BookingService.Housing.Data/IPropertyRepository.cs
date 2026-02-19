using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task UpdateNameAsync(Guid id, Guid ownerId, string name);
    Task UpdateDescriptionAsync(Guid id, Guid ownerId, string description);
    Task UpdateTagsAsync(Guid id, Guid ownerId, List<Guid> tags);
}