using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public class UnitRepository(BookingServiceDbContext context)
    : Repository<Unit>(context), IUnitRepository
{
    public async Task<Unit?> GetUnitAsync(int unitId)
    {
        return await DbSet.FindAsync(unitId);
    }
}