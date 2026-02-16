using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class StayRepository(BookingServiceDbContext context)
    : BaseRepository<Stay>(context), IStayRepository
{
    public async Task UpdateStatusAsync(int stayId, StayStatus status)
    {
        var stay = await DbSet.SingleOrDefaultAsync(s => s.Id == stayId);
        if (stay != null)
        {
            stay.Status = status;
            await Context.SaveChangesAsync();
        }
    }
}