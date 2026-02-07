using BookingService.Housing.DTOs.Unit;

namespace BookingService.Housing.Services;

public interface IUnitCustomizationService
{
    Task<List<UnitCustomizationDto>> GetUnitCustomizationsAsync(int id);
    Task AddUnitCustomizationsAsync(int id, List<UnitCustomizationCreationDto> creationList);
    Task UpdateUnitCustomizationsAsync(int id, List<UnitCustomizationUpdateDto> updateList);
    Task DeleteUnitCustomizationsAsync(int id);
}