using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services.RangeServices;

public interface IUnitCustomizationService : IBaseRangeService<UnitCustomizationCreationDto, UnitCustomizationUpdateDto>;