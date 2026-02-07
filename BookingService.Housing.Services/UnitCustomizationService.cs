using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Extensions;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitCustomizationService(IUnitCustomizationRepository repository) : IUnitCustomizationService
{
    public async Task<List<UnitCustomizationDto>> GetUnitCustomizationsAsync(int id)
    {
        var customizations = await repository.GetCustomizationsByUnitIdAsync(id);
        return customizations.ToUnitCustomizationDtoList();
    }

    public async Task AddUnitCustomizationsAsync(int id, List<UnitCustomizationCreationDto> creationList)
    {
        var customizations = creationList
            .Select(c => c.ToUnitCustomization(id))
            .ToList();
        
        await repository.AddAsync(customizations);
    }

    public async Task UpdateUnitCustomizationsAsync(int id, List<UnitCustomizationUpdateDto> updateList)
    {
        var customizations = updateList
            .Select(c => c.ToUnitCustomization(id))
            .ToList();
        
        await repository.UpdateAsync(customizations);
    }

    public async Task DeleteUnitCustomizationsAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}