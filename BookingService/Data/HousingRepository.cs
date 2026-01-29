using BookingService.Housing.Data;
using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class HousingRepository(BookingDbContext context) : IHousingRepository
{
    public async Task<IEnumerable<HousingInfo>> GetAll()
    {
        return await context.Housings.ToListAsync();
    }

    public async Task<HousingInfo?> GetById(int id)
    {
        return await context.Housings.SingleOrDefaultAsync(h => h.Id == id);
    }

    public async Task Create(HousingInfo housing)
    {
        context.Housings.Add(housing);
        await context.SaveChangesAsync();
    }

    public async Task Update(HousingInfo housing)
    {
        context.Housings.Update(housing);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        HousingInfo? housing = await GetById(id);
        if (housing is not null)
        {
            context.Housings.Remove(housing);
            await context.SaveChangesAsync();
        }
    }
}