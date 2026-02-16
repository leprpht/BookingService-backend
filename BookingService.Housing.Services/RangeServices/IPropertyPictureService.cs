using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services.RangeServices;

public interface IPropertyPictureService : IBaseRangeService<PropertyPictureCreationDto, PropertyPictureUpdateDto>;