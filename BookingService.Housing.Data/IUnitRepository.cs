using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IUnitRepository : IBaseRepository<Unit>
{
    Task UpdateNameAsync(int id, string name);
}