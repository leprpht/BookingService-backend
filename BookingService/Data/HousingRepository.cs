using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class HousingRepository(BookingDbContext context) : IHousingRepository
{
    public async Task<HousingInfoDto?> GetById(int id, PeriodRequest period)
    {
        var housing = await context.Housings
            .Include(h => h.Stays)
            .SingleOrDefaultAsync(h => h.Id == id);
        
        if (housing is null)
            return null;

        return new HousingInfoDto
        {
            Id = housing.Id,
            Name = housing.Name,
            Address = housing.Address,
            City = housing.City,
            State = housing.State,
            Country = housing.Country,
            Available = housing.Capacity - housing.Stays
                .Count(stay => stay.From < period.To && stay.To > period.From)
        };
    }

    public async Task<IEnumerable<HousingInfoDto>> GetByFilters(
        FilterOptions filter,
        PageRequest page)
    {
        return await context.Housings
            .Where(h =>
                (filter.Name == null || h.Name.Contains(filter.Name)) &&
                (filter.City == null || h.City == filter.City) &&
                (filter.Country == null || h.Country == filter.Country) &&
                (filter.MinPrice == null || h.Price >= filter.MinPrice) &&
                (filter.MaxPrice == null || h.Price <= filter.MaxPrice)
            )
            .Select(h => new HousingInfoDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City,
                State = h.State,
                Country = h.Country,

                Available = h.Capacity - h.Stays
                    .Count(stay => stay.From < filter.To && stay.To > filter.From)
            })
            .Where(h => h.Available > 0)
            .Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize)
            .ToListAsync();
    }

    public async Task Create(HousingInfo housing)
    {
        context.Housings.Add(housing);
        await context.SaveChangesAsync();
    }

    public async Task Update(HousingUpdateDto housing)
    {
        var housingEntity = await GetById(housing.Id);
        if (housingEntity is not null)
        {
            var updatedHousing = new HousingInfo
            {
                Id = housingEntity.Id,
                Name = housing.Name,
                Address = housing.Address,
                City = housing.City,
                State = housing.State,
                Country = housing.Country,
                Capacity = housing.Capacity,
                Price = housing.Price,
                Stays = housingEntity.Stays
            };
            context.Housings.Update(updatedHousing);
            await context.SaveChangesAsync();
        }
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
    
    public async Task<HousingInfoDto?> GetHousingByStayId(int id)
    {
        return await context.Housings
            .Where(h => h.Stays.Any(s => s.Id == id))
            .Select(h => new HousingInfoDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City,
                State = h.State,
                Country = h.Country
            })
            .FirstOrDefaultAsync();
    }
    
    private async Task<HousingInfo?> GetById(int id)
    {
        return await context.Housings
            .Include(h => h.Stays)
            .SingleOrDefaultAsync(h => h.Id == id);
    }
}