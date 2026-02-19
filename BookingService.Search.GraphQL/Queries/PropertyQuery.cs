using BookingService.Database;
using BookingService.Search.GraphQL.Types.Property;
using BookingService.Search.GraphQL.Types.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Search.GraphQL.Queries;

public class PropertyQuery
{
    [UseFiltering]
    [UseSorting]
    public IQueryable<PropertyPageType> SearchProperties(
        [Service] BookingServiceDbContext context,
        HousingFilterOptions filter,
        PageRequest page)
    {
        return context.Properties
            .Where(p => p.IsActive)
            .Where(p => p.Units
                .Any(u => !u.Stays
                    .Any(s => s.Status != StayStatus.Cancelled && s.From < filter.Period.To && s.To > filter.Period.From)))
            .Where(p => string.IsNullOrEmpty(filter.Name) || p.Name.Contains(filter.Name))
            .Where(p => string.IsNullOrEmpty(filter.City) || p.City.Contains(filter.City))
            .Where(p => string.IsNullOrEmpty(filter.Country) || p.Country.Contains(filter.Country))
            .Where(p => !filter.MinPrice.HasValue || p.Units.Max(u => u.Price) >= filter.MinPrice.Value)
            .Where(p => !filter.MaxPrice.HasValue || p.Units.Min(u => u.Price) <= filter.MaxPrice.Value)
            .Where(p => filter.Tags == null || filter.Tags.Count == 0 || p.Tags.Any(t => filter.Tags.Contains(t.Id)))
            .Where(p => !filter.MinRating.HasValue || p.AverageRating >= filter.MinRating.Value)
            .Select(p => new PropertyPageType 
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                City = p.City,
                State = p.State,
                Country = p.Country,
                Price = p.Units.Min(u => u.Price) * filter.Period.DaysCount,
                PictureUrl = p.Pictures.FirstOrDefault(pic => pic.IsCover)!.Url,
                Rating = p.AverageRating,
                RankingScore = p.RankingScore,
                ReviewCount = p.ReviewCount,
                AvailableUnits = p.Units
                    .Count(u => !u.Stays
                        .Any(s => s.Status != StayStatus.Cancelled && s.From < filter.Period.To && s.To > filter.Period.From)),
                Tags = p.Tags.Select(t => t.Text)
            })
            .OrderByDescending(p => p.RankingScore)
            .Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize);
    }

    public async Task<PropertyType?> GetPropertyDetails(
        [Service] BookingServiceDbContext context,
        Guid propertyId,
        PeriodRequest period)
    {
        return await context.Properties
            .Where(p => p.Id == propertyId)
            .Select(property => new PropertyType 
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                City = property.City,
                State = property.State,
                Country = property.Country,
                Description = property.Description,
                AverageRating = property.AverageRating,
                ReviewCount = property.ReviewCount,
                Pictures = property.Pictures
                    .OrderByDescending(p => p.IsCover)
                    .Select(p => p.Url)
                    .ToList(),
                Units = property.Units
                    .Select(u => new UnitListType
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Capacity = u.Capacity,
                        Price = u.Price * period.DaysCount,
                        Size = u.Size,
                        IsAvailable = !u.Stays
                            .Any(s => s.Status != StayStatus.Cancelled && s.From < period.To && s.To > period.From)
                    })
                    .OrderBy(u => u.Price)
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}
