using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Extensions;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;
using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public class PropertyService(IPropertyRepository repository, IMapper mapper)
    : BaseService<Property, PropertyCreationDto, PropertyUpdateDto>(repository), IPropertyService
{
    protected override Property MapCreate(PropertyCreationDto dto) => dto.ToProperty(mapper);
    protected override Property MapUpdate(PropertyUpdateDto dto) => dto.ToProperty(mapper);
    
    public async Task UpdateNameAsync(int id, string name) =>
        await repository.UpdateNameAsync(id, name);
    
    public async Task UpdateDescriptionAsync(int id, string description) =>
        await repository.UpdateDescriptionAsync(id, description);
    
    public async Task UpdateTagsAsync(int id, List<int> tags) =>
        await repository.UpdateTagsAsync(id, tags);
}