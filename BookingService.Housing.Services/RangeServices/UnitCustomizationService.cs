using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Housing.Services.RangeServices;

public class UnitCustomizationService(IUnitCustomizationRepository repository, IMapper mapper)
    : BaseRangeService<UnitCustomization, UnitCustomizationCreationDto, UnitCustomizationUpdateDto>(repository), IUnitCustomizationService
{
    protected override UnitCustomization MapCreate(Guid id, UnitCustomizationCreationDto dto) => dto.ToUnitCustomization(id, mapper);
    protected override UnitCustomization MapUpdate(Guid id, UnitCustomizationUpdateDto dto) => dto.ToUnitCustomization(id, mapper);
}