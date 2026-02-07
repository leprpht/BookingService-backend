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

    public override async Task DeleteAsync(int id)
    {
        Context.UnitCustomizations
            .RemoveRange(Context.UnitCustomizations
                .Where(u => u.UnitId == id));
        await Context.SaveChangesAsync();

        Context.UnitPictures
            .RemoveRange(Context.UnitPictures
                .Where(u => u.UnitId == id));
        await Context.SaveChangesAsync();

        await base.DeleteAsync(id);
    }
}