using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitService(IUnitRepository repository, IMapper mapper)
    : BaseService<Unit, UnitCreationDto, UnitUpdateDto>(repository), IUnitService
{
    protected override Unit MapCreate(Guid id, UnitCreationDto dto) => dto.ToUnit(id, mapper);
    protected override Unit MapUpdate(Guid id, UnitUpdateDto dto) => dto.ToUnit(id, mapper);
    
    public async Task ToggleAllUnitsActiveStatusAsync(Guid propertyId, Guid userId) =>
        await repository.ToggleAllUnitsActiveStatusAsync(propertyId, userId);
    
    public async Task ToggleActiveStatusAsync(Guid id, Guid userId) =>
        await repository.ToggleActiveStatusAsync(id, userId);
    
    public async Task UpdateNameAsync(Guid id, Guid userId, string name) =>
        await repository.UpdateNameAsync(id, userId, name);
}