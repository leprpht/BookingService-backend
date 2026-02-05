using BookingService.Housing.DTOs.Unit;
using BookingService.Shared;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitService(IUnitRepository repository) : IUnitService
{
    public async Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest)
    {
        var unit = await repository.GetUnitAsync(unitId);
        return unit?.ToUnitDto(periodRequest);
    }

    public async Task CreateUnitAsync(UnitCreationDto createUnitDto)
    {
        var unit = createUnitDto.ToUnit();
        await repository.AddAsync(unit);
    }

    public async Task UpdateUnitAsync(UnitUpdateDto unitUpdateDto)
    {
        var unit = unitUpdateDto.ToUnit();
        await repository.UpdateAsync(unit);
    }

    public async Task DeleteUnitAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}