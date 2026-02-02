using BookingService.Housing.DTOs;
using BookingService.Housing.DTOs.Unit;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public interface IUnitService
{
    Task<UnitDto?> GetUnitDetailsAsync(int unitId, PeriodRequest periodRequest);
    Task CreateUnitAsync(UnitCreationDto createUnitDto);
    Task UpdateUnitAsync(UnitUpdateDto unitUpdateDto);
    Task DeleteUnitAsync(int id);
}