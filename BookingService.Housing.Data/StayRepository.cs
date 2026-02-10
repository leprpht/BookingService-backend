using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class StayRepository(BookingServiceDbContext context)
    : BaseRepository<Stay>(context), IStayRepository
{
    public async Task<List<(Stay Stay, string Property, string Unit)>> GetStays(
        int guestId,
        PeriodRequest periodRequest,
        PageRequest pageRequest)
    {
        return (await DbSet
                .Where(s => s.UserId == guestId &&
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
        var result = await DbSet
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