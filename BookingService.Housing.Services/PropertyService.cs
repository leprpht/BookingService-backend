using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Location;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class PropertyService(
    IPropertyRepository repository,
    IMapper mapper,
    ILocationNormalizationService locationNormalization)
    : BaseService<Property, PropertyCreationDto, PropertyUpdateDto>(repository), IPropertyService
{
    protected override Property MapCreate(Guid userId, PropertyCreationDto dto)
    {
        return dto.ToProperty(userId, mapper);
    }

    protected override Property MapUpdate(Guid userId, PropertyUpdateDto dto)
    {
        return dto.ToProperty(userId, mapper);
    }

    public override async Task CreateAsync(Guid userId, PropertyCreationDto dto)
    {
        var property = MapCreate(userId, dto);
        await ApplyNormalizedLocationAsync(property);
        await repository.AddAsync(property);
    }

    public override async Task UpdateAsync(Guid userId, PropertyUpdateDto dto)
    {
        var property = MapUpdate(userId, dto);
        await ApplyNormalizedLocationAsync(property);
        await repository.UpdateAsync(property);
    }

    public async Task UpdateNameAsync(Guid id, Guid ownerId, string name)
    {
        await repository.UpdateNameAsync(id, ownerId, name);
    }

    public async Task UpdateDescriptionAsync(Guid id, Guid ownerId, string description)
    {
        await repository.UpdateDescriptionAsync(id, ownerId, description);
    }

    public async Task UpdateTagsAsync(Guid id, Guid ownerId, List<Guid> tags)
    {
        await repository.UpdateTagsAsync(id, ownerId, tags);
    }

    // ── private helpers ──────────────────────────────────────────────────

    /// <summary>
    /// Resolves the canonical city/state/country names via GeoNames and writes
    /// them back onto the property entity before it is persisted.
    /// Falls back to the original user input when the API is unavailable.
    /// </summary>
    private async Task ApplyNormalizedLocationAsync(Property property)
    {
        var result = await locationNormalization.NormalizeAsync(
            property.City,
            property.State,
            property.Country);

        property.City = result.City;
        property.State = result.State ?? property.State;
        property.Country = result.Country;
    }
}