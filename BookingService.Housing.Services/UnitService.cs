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
    protected override Unit MapCreate(UnitCreationDto dto) => dto.ToUnit(mapper);
    protected override Unit MapUpdate(UnitUpdateDto dto) => dto.ToUnit(mapper);
    
    public async Task UpdateNameAsync(int id, string name) =>
        await repository.UpdateNameAsync(id, name);
}