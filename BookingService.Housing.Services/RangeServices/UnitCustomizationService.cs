using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Service;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Housing.Services.RangeServices;

public class UnitCustomizationService(IUnitCustomizationRepository repository, IMapper mapper)
    : BaseSubservice<UnitCustomization, UnitCustomizationCreationDto, UnitCustomizationUpdateDto>(repository), IUnitCustomizationService
{
    protected override UnitCustomization MapCreate(int id, UnitCustomizationCreationDto dto) => dto.ToUnitCustomization(id, mapper);
    protected override UnitCustomization MapUpdate(int id, UnitCustomizationUpdateDto dto) => dto.ToUnitCustomization(id, mapper);
    
    public async Task<List<UnitCustomizationDto>> GetUnitCustomizationsAsync(int id)
    {
        var customizations = await repository.GetAllAsync(id);
        return customizations.ToUnitCustomizationDtoList();
    }
}