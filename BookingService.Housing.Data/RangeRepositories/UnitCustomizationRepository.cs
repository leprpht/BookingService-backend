using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data.RangeRepositories;

public class UnitCustomizationRepository(BookingServiceDbContext context)
    : BaseRangeRepository<UnitCustomization, Unit>(context), IUnitCustomizationRepository
{
    protected override Expression<Func<UnitCustomization, bool>> ByParentId(Guid unitId)
    {
        return uc => uc.UnitId == unitId;
    }

    protected override string ParentIdPropertyName => "UnitId";
}