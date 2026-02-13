using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.RangeServices;

public interface IPropertyPictureService : IBaseSubservice<PropertyPictureCreationDto, PropertyPictureUpdateDto>;