using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Housing.Services.RangeServices;

public class PropertyPictureService(IPropertyPictureRepository repository, IMapper mapper) : BaseRangeService<PropertyPicture, PropertyPictureCreationDto, PropertyPictureUpdateDto>(repository), IPropertyPictureService
{
    protected override PropertyPicture MapCreate(int id, PropertyPictureCreationDto dto) => dto.ToPropertyPicture(id, mapper);
    protected override PropertyPicture MapUpdate(int id, PropertyPictureUpdateDto dto) => dto.ToPropertyPicture(id, mapper);
}