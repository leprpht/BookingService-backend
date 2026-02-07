using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class UnitCustomizationRepository(BookingServiceDbContext context)
    : BaseRepository<List<UnitCustomization>>(context), IUnitCustomizationRepository
{
    public async Task<List<UnitCustomization>> GetCustomizationsByUnitIdAsync(int id)
    {
        return await Context.UnitCustomizations
            .Where(uc => uc.UnitId == id)
            .ToListAsync();
    }

    public override async Task AddAsync(List<UnitCustomization> entity)
    {
        if (entity.Count == 0)
            return;
        
        await Context.UnitCustomizations.AddRangeAsync(entity);
        await Context.SaveChangesAsync();
    }
    
    public override async Task UpdateAsync(List<UnitCustomization> entity)
    {
        if (entity.Count == 0)
            return;
        
        var updateIds = entity.Select(e => e.Id).ToHashSet();
        var customizationsToDelete = await Context.UnitCustomizations
            .Where(uc => uc.UnitId == entity.First().UnitId
                         && !updateIds.Contains(uc.Id))
            .ToListAsync();
        
        Context.UnitCustomizations.RemoveRange(customizationsToDelete);
        Context.UnitCustomizations.UpdateRange(entity);
        await Context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(int id)
    {
        Context.UnitCustomizations
            .RemoveRange(Context.UnitCustomizations
                .Where(u => u.UnitId == id));
        await Context.SaveChangesAsync();
    }
}