using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public class UnitAdditionalServicesRepository(BookingServiceDbContext context)
    : BaseRepository<UnitAdditionalServices>(context), IUnitAdditionalServicesRepository
{
    public override async Task UpdateAsync(UnitAdditionalServices entity)
    {
        var existingEntity = await DbSet.FindAsync(entity.Id);
        
        if (existingEntity == null)
            throw new NotFoundException("Additional service not found.");
        
        if (entity.Price != null)
            existingEntity.Price = entity.Price;
        
        if (entity.Name != null)
            existingEntity.Name = entity.Name;
        
        await Context.SaveChangesAsync();
    }
}