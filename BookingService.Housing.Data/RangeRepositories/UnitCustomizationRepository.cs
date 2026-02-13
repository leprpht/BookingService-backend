using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data.RangeRepositories;

public class UnitCustomizationRepository(BookingServiceDbContext context)
    : BaseRangeRepository<UnitCustomization>(context), IUnitCustomizationRepository
{
    protected override Expression<Func<UnitCustomization, bool>> ByParentId(int unitId) 
        => uc => uc.UnitId == unitId;
    
    protected override string ParentIdPropertyName => "UnitId";
}