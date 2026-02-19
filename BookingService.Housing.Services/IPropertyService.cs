using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IPropertyService : IBaseService<Property, PropertyCreationDto, PropertyUpdateDto>
{
    Task UpdateNameAsync(Guid id, Guid ownerId, string name);
    Task UpdateDescriptionAsync(Guid id, Guid ownerId, string description);
    Task UpdateTagsAsync(Guid id, Guid ownerId, List<Guid> tags);
}