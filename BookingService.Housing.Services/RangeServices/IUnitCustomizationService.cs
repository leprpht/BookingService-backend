using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.RangeServices;

public interface IUnitCustomizationService : IBaseRangeService<UnitCustomizationCreationDto, UnitCustomizationUpdateDto>;