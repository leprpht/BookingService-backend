using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.Subservices;

public interface IPropertyPictureService : IBaseSubservice<PropertyPictureCreationDto, PropertyPictureUpdateDto>;