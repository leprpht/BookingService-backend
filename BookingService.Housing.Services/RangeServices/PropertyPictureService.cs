using AutoMapper;
using BookingService.Database;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data.RangeRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Housing.Services.RangeServices;

public class PropertyPictureService(
    IPropertyPictureRepository repository,
    IMapper mapper,
    BookingServiceDbContext context)
    : BaseRangeService<PropertyPicture, PropertyPictureCreationDto, PropertyPictureUpdateDto>(repository),
        IPropertyPictureService
{
    protected override PropertyPicture MapCreate(Guid id, PropertyPictureCreationDto dto)
    {
        return dto.ToPropertyPicture(id, mapper);
    }

    protected override PropertyPicture MapUpdate(Guid id, PropertyPictureUpdateDto dto)
    {
        return dto.ToPropertyPicture(id, mapper);
    }

    protected override async Task<Guid?> GetOwnerIdAsync(Guid propertyId)
    {
        return await context.Properties
            .Where(p => p.Id == propertyId)
            .Select(p => (Guid?)p.OwnerId)
            .FirstOrDefaultAsync();
    }
}