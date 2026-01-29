using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class HousingRepository(BookingDbContext context) : IHousingRepository
{
    public async Task<IEnumerable<HousingInfo>> GetAll(int page, int pageSize)
    {
        return await context.Housings
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<HousingInfo?> GetById(int id)
    {
        return await context.Housings.SingleOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IEnumerable<HousingInfo>> GetByFilters(FilterOptions filter, int page, int pageSize)
    {
        return await context.Housings
            .Where(h =>
                (string.IsNullOrEmpty(filter.Name) || h.Name.Contains(filter.Name)) &&
                (string.IsNullOrEmpty(filter.City) || h.City.Contains(filter.City)) &&
                (string.IsNullOrEmpty(filter.Country) || h.Country.Contains(filter.Country)) &&
                (!filter.AvailableOnly.HasValue || h.Capacity > 0)
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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