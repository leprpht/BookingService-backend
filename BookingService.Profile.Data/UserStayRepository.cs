using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Profile.Data;

public class UserStayRepository(BookingServiceDbContext context) : IUserStayRepository
{
    public async Task<List<Stay>> GetUserStays(Guid userId, int page, int pageSize, StaySearchFilter filter)
    {
        return await context.Stays
            .Where(s => s.UserId == userId)
            .Where(s => filter.Status == null || filter.Status == s.Status)
            .Where(s => filter.From == null || s.From >= filter.From)
            .Where(s => filter.To == null || s.To <= filter.To)
            .Include(s => s.RoomInstance)
            .ThenInclude(r => r.Unit)
            .ThenInclude(u => u.Property)
            .Where(s => filter.SearchTerm == null
                        || s.RoomInstance.Unit.Name.Contains(filter.SearchTerm)
                        || s.RoomInstance.RoomNumber.Contains(filter.SearchTerm)
                        || s.RoomInstance.Unit.Property.Address.Contains(filter.SearchTerm)
                        || s.RoomInstance.Unit.Property.City.Contains(filter.SearchTerm)
                        || s.RoomInstance.Unit.Property.State.Contains(filter.SearchTerm)
                        || s.RoomInstance.Unit.Property.Country.Contains(filter.SearchTerm)
                        || s.RoomInstance.Unit.Property.Name.Contains(filter.SearchTerm))
            .OrderByDescending(s => s.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}