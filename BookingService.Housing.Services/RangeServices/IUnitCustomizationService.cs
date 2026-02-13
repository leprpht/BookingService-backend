using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.RangeServices;

public interface IUnitCustomizationService : IBaseSubservice<UnitCustomizationCreationDto, UnitCustomizationUpdateDto>
{
    Task<List<UnitCustomizationDto>> GetUnitCustomizationsAsync(int id);
}