using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class StayRepository(BookingServiceDbContext context)
    : BaseRepository<Stay>(context), IStayRepository
{
    public async Task<Stay?> GetByUserIdAsync(Guid userId)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task UpdateStatusAsync(Guid stayId, StayStatus status)
    {
        var stay = await DbSet.FirstOrDefaultAsync(s => s.Id == stayId);
        
        if (stay == null)
            throw new NotFoundException("Stay not found.");
        
        stay.Status = status;
        await Context.SaveChangesAsync();
    }
}