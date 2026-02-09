using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Service;
using BookingServices.Housing.Data.Subrepositories;

namespace BookingService.Housing.Services.Subservices;

public class PropertyPictureService(IPropertyPictureRepository repository, IMapper mapper) : BaseSubservice<PropertyPicture, PropertyPictureCreationDto, PropertyPictureUpdateDto>(repository), IPropertyPictureService
{
    protected override PropertyPicture MapCreate(int id, PropertyPictureCreationDto dto) => dto.ToPropertyPicture(id, mapper);
    protected override PropertyPicture MapUpdate(int id, PropertyPictureUpdateDto dto) => dto.ToPropertyPicture(id, mapper);
}