using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class StayRepository(BookingServiceDbContext context)
    : BaseRepository<Stay>(context), IStayRepository
{
    public async Task<List<Stay>> GetByUserIdAsync(Guid userId) =>
        await DbSet
            .Where(s => s.UserId == userId)
            .Include(s => s.RoomInstance)
            .ThenInclude(r => r.Unit)
            .ThenInclude(u => u.Property)
            .ToListAsync();

    public async Task UpdateStatusAsync(Guid stayId, StayStatus status)
    {
        var stay = await DbSet.FindAsync(stayId)
                   ?? throw new NotFoundException("Stay not found.");

        stay.Status = status;
        await Context.SaveChangesAsync();
    }

    public new async Task<Stay> GetByIdAsync(Guid id) =>
        await DbSet
            .Include(s => s.RoomInstance)
            .ThenInclude(r => r.Unit)
            .FirstOrDefaultAsync(s => s.Id == id)
        ?? throw new NotFoundException("Stay not found.");
}