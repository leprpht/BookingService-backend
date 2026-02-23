using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class UnitRepository(BookingServiceDbContext context)
    : BaseRepository<Unit>(context), IUnitRepository
{
    public async Task ToggleAllUnitsActiveStatusAsync(Guid propertyId, Guid userId)
    {
        var units = await DbSet
            .Where(u => u.PropertyId == propertyId)
            .ToListAsync();
        
        if (units.Count == 0)
            throw new NotFoundException("No units found for the specified property.");

        var newStatus = !units.Any(u => u.IsActive);
        
        foreach (var unit in units)
        {
            if (unit.OwnerId != userId)
                throw new ForbidException();
            unit.IsActive = newStatus;
        }
        
        await Context.SaveChangesAsync();
    }
    
    public async Task ToggleActiveStatusAsync(Guid unitId, Guid userId)
    {
        var unit = await DbSet.FirstOrDefaultAsync(u => u.Id == unitId);
        
        if (unit == null)
            throw new NotFoundException("Unit not found.");
        
        if (unit.OwnerId != userId)
            throw new ForbidException();
        
        unit.IsActive = !unit.IsActive;
        await Context.SaveChangesAsync();
    }
    
    public async Task UpdateNameAsync(Guid unitId, Guid userId, string name)
    {
        var unit = await DbSet.FirstOrDefaultAsync(u => u.Id == unitId);
        
        if (unit == null)
            throw new NotFoundException("Unit not found.");
        
        if (unit.OwnerId != userId)
            throw new ForbidException();
        
        unit.Name = name;
        await Context.SaveChangesAsync();
    }
    
    public override async Task DeleteAsync(Guid id, Guid ownerId)
    {
        Context.UnitCustomizations
            .RemoveRange(Context.UnitCustomizations
                .Where(u => u.UnitId == id));
        await Context.SaveChangesAsync();

        Context.UnitPictures
            .RemoveRange(Context.UnitPictures
                .Where(u => u.UnitId == id));
        await Context.SaveChangesAsync();

        await base.DeleteAsync(id, ownerId);
    }
}