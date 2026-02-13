using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class UnitAdditionalServicesRepository(BookingServiceDbContext context)
    : BaseRepository<UnitAdditionalServices>(context), IUnitAdditionalServicesRepository
{
    public async Task<List<UnitAdditionalServices>> GetUnitAdditionalServicesAsync(int id) => 
        await Context.AdditionalServices
            .Where(s => s.UnitId == id)
            .ToListAsync();

    public override async Task UpdateAsync(UnitAdditionalServices entity)
    {
        var existingEntity = await DbSet.FindAsync(entity.Id);
        
        if (existingEntity == null)
            return;
        
        if (entity.Price != null)
            existingEntity.Price = entity.Price;
        
        if (entity.Name != null)
            existingEntity.Name = entity.Name;
        
        await Context.SaveChangesAsync();
    }
}