using AutoMapper;
using BookingService.Database;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data.RangeRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Housing.Services.RangeServices;

public class UnitCustomizationService(
    IUnitCustomizationRepository repository,
    IMapper mapper,
    BookingServiceDbContext context)
    : BaseRangeService<UnitCustomization, UnitCustomizationCreationDto, UnitCustomizationUpdateDto>(repository),
        IUnitCustomizationService
{
    protected override UnitCustomization MapCreate(Guid id, UnitCustomizationCreationDto dto)
    {
        return dto.ToUnitCustomization(id, mapper);
    }

    protected override UnitCustomization MapUpdate(Guid id, UnitCustomizationUpdateDto dto)
    {
        return dto.ToUnitCustomization(id, mapper);
    }

    protected override async Task<Guid?> GetOwnerIdAsync(Guid unitId)
    {
        return await context.Units
            .Where(u => u.Id == unitId)
            .Select(u => (Guid?)u.OwnerId)
            .FirstOrDefaultAsync();
    }
}