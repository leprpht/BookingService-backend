using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data.RangeRepositories;

public class PropertyPictureRepository(BookingServiceDbContext context)
    : BaseRangeRepository<PropertyPicture, Property>(context), IPropertyPictureRepository
{
    protected override Expression<Func<PropertyPicture, bool>> ByParentId(Guid propertyId)
    {
        return pp => pp.PropertyId == propertyId;
    }

    protected override string ParentIdPropertyName => "PropertyId";
}