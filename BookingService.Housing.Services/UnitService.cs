using BookingService.Housing.DTOs;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitService(IUnitRepository repository) : IUnitService
{
    public async Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest)
    {
        var unit = await repository.GetUnitAsync(unitId, periodRequest);
        return unit?.ToUnitDto(periodRequest);
    }

    public async Task CreateUnitAsync(UnitCreationDto createUnitDto)
    {
        var unit = createUnitDto.ToUnit();
        await repository.CreateUnitAsync(unit);
    }

    public async Task UpdateUnitAsync(UnitUpdateDto unitUpdateDto)
    {
        var unit = unitUpdateDto.ToUnit();
        await repository.UpdateUnitAsync(unit);
    }

    public async Task DeleteUnitAsync(int id)
    {
        await repository.DeleteUnitAsync(id);
    }
}