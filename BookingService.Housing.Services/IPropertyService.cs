using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IPropertyService : IBaseService<PropertyCreationDto, PropertyUpdateDto>
{
    Task UpdateNameAsync(int id, string name);
    Task UpdateDescriptionAsync(int id, string description);
    Task UpdateTagsAsync(int id, List<int> tags);
}