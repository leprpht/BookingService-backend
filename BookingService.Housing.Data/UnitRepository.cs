using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class UnitRepository(BookingServiceDbContext context)
    : BaseRepository<Unit>(context), IUnitRepository
{
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
    
    public async Task UpdateNameAsync(int unitId, string name)
    {
        var unit = await DbSet.FirstOrDefaultAsync(u => u.Id == unitId);
        
        if (unit == null)
            throw new NotFoundException("Unit not found.");
        
        unit.Name = name;
        await Context.SaveChangesAsync();
    }
}