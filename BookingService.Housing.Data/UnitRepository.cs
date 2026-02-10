using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class UnitRepository(BookingServiceDbContext context)
    : BaseRepository<Unit>(context), IUnitRepository
{
    public async Task<Unit?> GetUnitAsync(int unitId)
    {
        return await DbSet
            .Where(u => u.IsActive)
            .Include(u => u.Customizations)
            .Include(u => u.Pictures)
            .SingleOrDefaultAsync(u => u.Id == unitId);
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
    
    public async Task UpdateNameAsync(int propertyId, string name)
    {
        var property = await DbSet.SingleOrDefaultAsync(p => p.Id == propertyId);
        if (property != null)
        {
            property.Name = name;
            await Context.SaveChangesAsync();
        }
    }
}