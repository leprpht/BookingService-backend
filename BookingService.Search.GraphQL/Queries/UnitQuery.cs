using BookingService.Database;
using BookingService.Search.GraphQL.Types.Unit;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Search.GraphQL.Queries;

public class UnitQuery
{
    public async Task<UnitType?> GetUnitByIdAsync(
        [Service] BookingServiceDbContext context,
        Guid unitId,
        PeriodRequest period)
    {
        return await context.Units
            .Where(u => u.Id == unitId
                        && u.Property.IsActive
                        && u.IsActive)
            .Select(u => new UnitType
            {
                Id = u.Id,
                Name = u.Name,
                Capacity = u.Capacity,
                Price = u.Price * period.DaysCount,
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
                        Text = c.Select(t => t.Text).ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}