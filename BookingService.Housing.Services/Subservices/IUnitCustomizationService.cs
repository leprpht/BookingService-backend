using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services.Subservices;

public interface IUnitCustomizationService : IBaseSubservice<UnitCustomizationCreationDto, UnitCustomizationUpdateDto>
{
    Task<List<UnitCustomizationDto>> GetUnitCustomizationsAsync(int id);
}