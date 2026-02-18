using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IPropertyService : IBaseService<Property, PropertyCreationDto, PropertyUpdateDto>
{
    Task UpdateNameAsync(int id, int ownerId, string name);
    Task UpdateDescriptionAsync(int id, int ownerId, string description);
    Task UpdateTagsAsync(int id, int ownerId, List<int> tags);
}