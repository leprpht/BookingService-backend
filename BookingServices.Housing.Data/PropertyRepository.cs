using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class PropertyRepository(BookingServiceDbContext context) : IPropertyRepository
{
    public async Task<List<Property>> GetAvailablePropertiesAsync(HousingFilterOptions housingFilterOptions, PageRequest pageRequest)
    {
        return await context.Properties
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
        return await context.Properties
            .Include(p => p.Units)
            .Include(p => p.Pictures)
            .Include(p => p.Reviews)
            .SingleOrDefaultAsync(p => p.Id == propertyId);
    }

    public async Task CreatePropertyAsync(Property property)
    {
        property.UpdateRating();
        await context.Properties.AddAsync(property);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePropertyAsync(Property property)
    {
        property.UpdateRating();
        context.Properties.Update(property);
        await context.SaveChangesAsync();
    }


    public async Task DeletePropertyAsync(int id)
    {
        var units = context.Units.Where(u => u.PropertyId == id);
        await units.ExecuteDeleteAsync();
        
        var reviews = context.PropertyReviews.Where(r => r.PropertyId == id);
        await reviews.ExecuteDeleteAsync();

        var pictures = context.PropertyPictures.Where(p => p.PropertyId == id);
        await pictures.ExecuteDeleteAsync();
        
        var property = await context.Properties.FindAsync(id);
        if (property != null)
        {
            context.Properties.Remove(property);
            await context.SaveChangesAsync();
        }
    }

    private static Expression<Func<Property, bool>> MatchesFilters(HousingFilterOptions filter)
    {
        return property => property.Units.Any(u => u.Stays.Any(s => s.Status != StayStatus.Cancelled && s.From < filter.Period.To && s.To > filter.Period.From)) &&
                           (string.IsNullOrEmpty(filter.Name) || property.Name.Contains(filter.Name)) &&
                           (string.IsNullOrEmpty(filter.City) || property.City.Contains(filter.City)) &&
                           (string.IsNullOrEmpty(filter.Country) || property.Country.Contains(filter.Country)) &&
                           (!filter.MinPrice.HasValue || property.Units.Max(u => u.Price) >= filter.MinPrice.Value) && 
                           (!filter.MaxPrice.HasValue || property.Units.Min(u => u.Price) <= filter.MaxPrice.Value);
    }
}