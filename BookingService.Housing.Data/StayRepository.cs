using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class StayRepository(BookingServiceDbContext context) : IStayRepository
{
    public async Task<List<(Stay Stay, string Property, string Unit)>> GetStays(
        int guestId,
        PeriodRequest periodRequest,
        PageRequest pageRequest)
    {
        return (await context.Stays
                .Where(s => s.GuestId == guestId &&
                            s.From >= periodRequest.From &&
                            s.To <= periodRequest.To)
                .Select(s => new
                {
                    Stay = s,
                    Property = s.Unit.Property.Name,
                    Unit = s.Unit.Name
                })
                .ToListAsync())
            .Select(x => (x.Stay, x.Property, x.Unit))
            .OrderByDescending(x => x.Stay.To)
            .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToList();
    }

    public async Task<(Stay? Stay, string Property, string Unit)> GetStayById(int stayId)
    {
        var result = await context.Stays
            .Where(s => s.Id == stayId)
            .Select(s => new
            {
                Stay = s,
                Property = s.Unit.Property.Name,
                Unit = s.Unit.Name
            })
            .SingleOrDefaultAsync();

        return result == null 
            ? (Stay: null, Property: string.Empty, Unit: string.Empty) 
            : (result.Stay, result.Property, result.Unit);
    }

    public async Task CreateStayAsync(Stay stay)
    {
        await context.Stays.AddAsync(stay);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStayAsync(Stay stay)
    {
        context.Stays.Update(stay);
        await context.SaveChangesAsync();
    }

    public async Task DeleteStayAsync(int id)
    {
        var stay = await context.Stays.FindAsync(id);
        if (stay != null)
        {
            context.Stays.Remove(stay);
            await context.SaveChangesAsync();
        }
    }
}