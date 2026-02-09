using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.Subservices;

public interface IUnitPictureService : IBaseSubservice<UnitPictureCreationDto, UnitPictureUpdateDto>;