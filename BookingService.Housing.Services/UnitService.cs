using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitService(IUnitRepository repository)
    : BaseService<Unit, UnitCreationDto, UnitUpdateDto>(repository), IUnitService
{
    public async Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest)
    {
        var unit = await repository.GetUnitAsync(unitId);
        return unit?.ToUnitDto(periodRequest);
    }
}