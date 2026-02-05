using BookingService.Database;
using BookingService.Housing.Models;

namespace BookingServices.Housing.Data;

public class UnitRepository(BookingServiceDbContext context) : IUnitRepository
{
    public async Task<Unit?> GetUnitAsync(int unitId)
    {
        return await context.Units.FindAsync(unitId);
    }

    public async Task CreateUnitAsync(Unit unit)
    {
        await context.Units.AddAsync(unit);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUnitAsync(Unit unit)
    {
        context.Units.Update(unit);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUnitAsync(int id)
    {
        var unit = await context.Units.FindAsync(id);
        if (unit != null)
        {
            context.Units.Remove(unit);
            await context.SaveChangesAsync();
        }
    }
}