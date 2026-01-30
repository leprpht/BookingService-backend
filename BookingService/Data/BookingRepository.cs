using BookingService.Housing.Data;
using BookingService.Housing.Models;
using BookingService.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class BookingRepository(BookingDbContext context) : IBookingRepository
{
    public async Task<Stay?> GetStayById(int id)
    {
        return await context.Stays.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Stay>> GetStaysByLocationId(int id, PeriodRequest period)
    {
        return await context.Stays
            .Where(s => s.HousingInfoId == id && s.From < period.To && s.To > period.From)
            .ToListAsync();
    }

    public async Task<List<Stay>> GetStaysByUserId(int id, PeriodRequest period)
    {
        return await context.Stays
            .Where(s => s.UserId == id && s.From < period.To && s.To > period.From)
            .ToListAsync();
    }
}