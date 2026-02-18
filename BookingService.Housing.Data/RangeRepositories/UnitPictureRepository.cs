using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data.RangeRepositories;

public class UnitPictureRepository(BookingServiceDbContext context)
    : BaseRangeRepository<UnitPicture, Unit>(context), IUnitPictureRepository
{
    protected override Expression<Func<UnitPicture, bool>> ByParentId(int unitId) 
        => up => up.UnitId == unitId;
    
    protected override string ParentIdPropertyName => "UnitId";
}