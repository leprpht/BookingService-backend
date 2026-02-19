using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;
using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public class PropertyService(IPropertyRepository repository, IMapper mapper)
    : BaseService<Property, PropertyCreationDto, PropertyUpdateDto>(repository), IPropertyService
{
    protected override Property MapCreate(Guid userId, PropertyCreationDto dto) => dto.ToProperty(userId, mapper);
    protected override Property MapUpdate(Guid userId, PropertyUpdateDto dto) => dto.ToProperty(userId, mapper);

    public async Task UpdateNameAsync(Guid id, Guid ownerId, string name) =>
        await repository.UpdateNameAsync(id, ownerId, name);
    
    public async Task UpdateDescriptionAsync(Guid id, Guid ownerId, string description) =>
        await repository.UpdateDescriptionAsync(id, ownerId, description);
    
    public async Task UpdateTagsAsync(Guid id, Guid ownerId, List<Guid> tags) =>
        await repository.UpdateTagsAsync(id, ownerId, tags);
}