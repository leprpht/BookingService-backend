using BookingService.Database;
using BookingService.Search.GraphQL.Types.Unit;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Search.GraphQL.Queries;

public class UnitQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<UnitType?> GetUnitByIdAsync(
        [Service] BookingServiceDbContext context,
        int unit,
        PeriodRequest period)
    {
        var dayCount = period.To.DayNumber - period.From.DayNumber;
        
        return await context.Units
            .Where(u => u.Id == unit && u.Property.IsActive && u.IsActive)
            .Select(u => new UnitType
            {
                Id = u.Id,
                Name = u.Name,
                Capacity = u.Capacity,
                Price = u.Price * dayCount,
                Size = u.Size,
                Pictures = u.Pictures
                    .OrderByDescending(p => p.IsCover)
                    .Select(p => p.Url)
                    .ToList(),
                Customizations = u.Customizations
                    .GroupBy(c => c.Type)
                    .Select(c => new UnitCustomizationType
                    {
                        Type = c.Key.ToString(),
                        Text = c
                            .Select(t => t.Text)
                            .ToList()
                    })
                    .ToList()
            })
            .SingleOrDefaultAsync();
    }
}