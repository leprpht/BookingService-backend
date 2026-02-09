using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class PropertyRepository(BookingServiceDbContext context)
    : BaseRepository<Property>(context), IPropertyRepository
{
    public async Task<List<Property>> GetAvailablePropertiesAsync(
        HousingFilterOptions housingFilterOptions,
        PageRequest pageRequest)
    {
        return await DbSet
            .Include(p => p.Units)
            .Include(p => p.Pictures)
            .Include(p => p.Reviews)
            .Where(MatchesFilters(housingFilterOptions))
            .OrderByDescending(p => p.RankingScore)
            .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToListAsync();
    }

    public async Task<Property?> GetPropertyAsync(int propertyId)
    {
        return await DbSet
            .Include(p => p.Units)
            .Include(p => p.Pictures)
            .Include(p => p.Reviews)
            .SingleOrDefaultAsync(p => p.Id == propertyId);
    }

    public override async Task AddAsync(Property property)
    {
        property.UpdateRating();
        await base.AddAsync(property);
    }

    public override async Task UpdateAsync(Property property)
    {
        property.UpdateRating();
        await base.UpdateAsync(property);
    }

    public override async Task DeleteAsync(int id)
    {
        await Context.Units
            .Where(u => u.PropertyId == id)
            .ExecuteDeleteAsync();

        await Context.PropertyReviews
            .Where(r => r.PropertyId == id)
            .ExecuteDeleteAsync();

        await Context.PropertyPictures
            .Where(p => p.PropertyId == id)
            .ExecuteDeleteAsync();

        await base.DeleteAsync(id);
    }

    private static Expression<Func<Property, bool>> MatchesFilters(HousingFilterOptions filter)
    {
        return property =>
            property.Units.Any(u =>
                u.Stays.Any(s =>
                    s.Status != StayStatus.Cancelled &&
                    s.From < filter.Period.To &&
                    s.To > filter.Period.From)) &&
            (string.IsNullOrEmpty(filter.Name) || property.Name.Contains(filter.Name)) &&
            (string.IsNullOrEmpty(filter.City) || property.City.Contains(filter.City)) &&
            (string.IsNullOrEmpty(filter.Country) || property.Country.Contains(filter.Country)) &&
            (!filter.MinPrice.HasValue || property.Units.Max(u => u.Price) >= filter.MinPrice.Value) &&
            (!filter.MaxPrice.HasValue || property.Units.Min(u => u.Price) <= filter.MaxPrice.Value);
    }
    
    public async Task UpdateNameAsync(int propertyId, string name)
    {
        var property = await DbSet.SingleOrDefaultAsync(p => p.Id == propertyId);
        if (property != null)
        {
            property.Name = name;
            await Context.SaveChangesAsync();
        }
    }
}

