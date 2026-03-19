using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class RoomInstanceRepository(BookingServiceDbContext context) : IRoomInstanceRepository
{
    public async Task<RoomInstance?> GetByIdAsync(Guid id)
    {
        return await context.RoomInstances.FindAsync(id);
    }

    public async Task<List<RoomInstance>> GetByUnitIdAsync(Guid unitId)
    {
        return await context.RoomInstances
            .Where(r => r.UnitId == unitId)
            .OrderBy(r => r.RoomNumber)
            .ToListAsync();
    }

    public async Task<RoomInstance?> FindAvailableAsync(Guid unitId, DateOnly from, DateOnly to)
    {
        return await context.RoomInstances
            .Include(r => r.Unit) // Required: StayService reads room.Unit.Price after this call
            .Where(r => r.UnitId == unitId && r.Status == RoomStatus.Available)
            .Where(r => !r.Stays.Any(s =>
                s.Status != StayStatus.Cancelled &&
                s.From < to &&
                s.To > from))
            .FirstOrDefaultAsync();
    }

    public async Task AddRangeAsync(Guid unitId, Guid ownerId, List<RoomInstance> rooms)
    {
        var unit = await context.Units.FindAsync(unitId)
                   ?? throw new NotFoundException("Unit not found.");

        if (unit.OwnerId != ownerId)
            throw new ForbidException();

        foreach (var room in rooms)
            room.UnitId = unitId;

        await context.RoomInstances.AddRangeAsync(rooms);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid id, RoomStatus status)
    {
        var room = await context.RoomInstances.FindAsync(id)
                   ?? throw new NotFoundException("Room instance not found.");

        room.Status = status;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id, Guid ownerId)
    {
        var room = await context.RoomInstances
                       .Include(r => r.Unit)
                       .FirstOrDefaultAsync(r => r.Id == id)
                   ?? throw new NotFoundException("Room instance not found.");

        if (room.Unit.OwnerId != ownerId)
            throw new ForbidException();

        var hasActiveStays = await context.Stays.AnyAsync(s =>
            s.RoomInstanceId == id &&
            s.Status != StayStatus.Cancelled &&
            s.Status != StayStatus.Completed);

        if (hasActiveStays)
            throw new InvalidOperationException("Cannot delete a room with active stays.");

        context.RoomInstances.Remove(room);
        await context.SaveChangesAsync();
    }
}