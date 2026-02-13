using BookingService.Housing.DTOs.Unit;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IUnitAdditionalService : IBaseService<UnitAdditionalServicesCreationDto, UnitAdditionalServicesUpdateDto>
{
    Task<List<UnitAdditionalServicesDto>> GetUnitAdditionalServicesAsync(int id);
}