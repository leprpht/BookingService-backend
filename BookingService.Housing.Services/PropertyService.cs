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
    protected override Property MapCreate(int userId, PropertyCreationDto dto) => dto.ToProperty(userId, mapper);
    protected override Property MapUpdate(int userId, PropertyUpdateDto dto) => dto.ToProperty(userId, mapper);
    

    public async Task UpdateNameAsync(int id, int ownerId, string name) =>
        await repository.UpdateNameAsync(id, ownerId, name);
    
    public async Task UpdateDescriptionAsync(int id, int ownerId, string description) =>
        await repository.UpdateDescriptionAsync(id, ownerId, description);
    
    public async Task UpdateTagsAsync(int id, int ownerId, List<int> tags) =>
        await repository.UpdateTagsAsync(id, ownerId, tags);
}