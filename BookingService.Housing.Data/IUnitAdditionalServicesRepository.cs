using BookingService.Housing.Models;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public interface IUnitAdditionalServicesRepository : IBaseRepository<UnitAdditionalServices>
{
    Task<List<UnitAdditionalServices>> GetUnitAdditionalServicesAsync(int id);
}