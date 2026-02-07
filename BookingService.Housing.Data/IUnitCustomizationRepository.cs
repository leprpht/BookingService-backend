using BookingService.Housing.Models;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public interface IUnitCustomizationRepository : IRepository<List<UnitCustomization>>
{
    Task<List<UnitCustomization>> GetCustomizationsByUnitIdAsync(int id);
}