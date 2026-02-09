using System.Linq.Expressions;
using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data.Subrepositories;

public class PropertyPictureRepository(BookingServiceDbContext context)
    : BaseSubrepository<PropertyPicture>(context), IPropertyPictureRepository
{
    protected override Expression<Func<PropertyPicture, bool>> ByParentId(int propertyId) 
        => pp => pp.PropertyId == propertyId;
    
    protected override string ParentIdPropertyName => "PropertyId";
}