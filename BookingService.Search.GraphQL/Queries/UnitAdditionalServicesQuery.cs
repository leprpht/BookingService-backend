using BookingService.Database;
using BookingService.Search.GraphQL.Types.Unit;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Search.GraphQL.Queries;

[ExtendObjectType("Query")]
public class UnitAdditionalServicesQuery
{
    public async Task<List<UnitAdditionalServicesType>> GetUnitAdditionalServicesAsync(
        [Service] BookingServiceDbContext context,
        Guid unitId)
    {
        return await context.AdditionalServices
            .Where(s => s.UnitId == unitId && s.Unit.IsActive)
            .Select(s => new UnitAdditionalServicesType
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price
            })
            .ToListAsync();
    }
}