using BookingService.Search.GraphQL.Types;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingServices.Housing.Data;
using BookingServices.Housing.Data.RangeRepositories;

namespace BookingService.Search.GraphQL;

public class HousingQuery
{
/*

// Property Queries
public async Task<List<PropertyPageType>> GetAvailableProperties(
    [Service] IPropertyRepository repository,
    HousingFilterInput filter,
    PageRequest page)
{
    var properties = await repository.GetAvailablePropertiesAsync(filter, page);

    return properties.Select(p =>
    {
        var period = new PeriodRequest { From = filter.Period.From, To = filter.Period.To };
        var minPrice = p.Units.Any() ? p.Units.Min(u => u.Price) : 0;

        return new PropertyPageType
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            City = p.City,
            State = p.State,
            Country = p.Country,
            Price = (decimal)(minPrice * period.DaysCount),
            PictureUrl = p.Pictures.FirstOrDefault(pic => pic.IsCover)?.Url,
            Rating = p.AverageRating,
            ReviewCount = p.ReviewCount
        };
    }).ToList();
}

public async Task<PropertyType?> GetPropertyDetails(
    [Service] IPropertyRepository repository,
    int propertyId,
    PeriodInput period)
{
    var property = await repository.GetPropertyAsync(propertyId);
    if (property == null) return null;

    var periodRequest = new PeriodRequest { From = period.From, To = period.To };

    return new PropertyType
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
        Units = property.Units.Select(u => new UnitListType
        {
            Id = u.Id,
            Name = u.Name,
            Capacity = u.Capacity,
            Price = (decimal)(u.Price * periodRequest.DaysCount),
            Size = u.Size,
            IsAvailable = !u.Stays.Any(s =>
                s.Status != StayStatus.Cancelled &&
                s.From < periodRequest.To &&
                s.To > periodRequest.From)
        }).ToList()
    };
}

// Unit Queries
public async Task<UnitType?> GetUnitDetails(
    [Service] IUnitRepository repository,
    int unitId,
    PeriodRequest period)
{
    var unit = await repository.GetUnitAsync(unitId);
    if (unit == null) return null;

    var periodRequest = new PeriodRequest { From = period.From, To = period.To };

    return new UnitType
    {
        Id = unit.Id,
        Name = unit.Name,
        Capacity = unit.Capacity,
        Price = (decimal)(unit.Price * periodRequest.DaysCount),
        Size = unit.Size,
        Customizations = unit.Customizations
            .GroupBy(c => c.Type)
            .Select(g => new UnitCustomizationType
            {
                Type = g.Key.ToString(),
                Customizations = g.Select(c => new UnitCustomizationGroupedType
                {
                    Id = c.Id,
                    Text = c.Text
                }).ToList()
            })
            .ToList(),
        Pictures = unit.Pictures.Select(p => p.Url).ToList()
    };
}

public async Task<List<UnitCustomizationType>> GetUnitCustomizations(
    [Service] IUnitCustomizationRepository repository,
    int unitId)
{
    var customizations = await repository.GetAllAsync(unitId);

    return customizations
        .GroupBy(c => c.Type)
        .Select(g => new UnitCustomizationType
        {
            Type = g.Key.ToString(),
            Customizations = g.Select(c => new UnitCustomizationGroupedType
            {
                Id = c.Id,
                Text = c.Text
            }).ToList()
        })
        .ToList();
}

public async Task<List<UnitAdditionalServicesType>> GetUnitAdditionalServices(
    [Service] IUnitAdditionalServicesRepository repository,
    int unitId)
{
    var services = await repository.GetUnitAdditionalServicesAsync(unitId);

    return services.Select(s => new UnitAdditionalServicesType
    {
        Id = s.Id,
        Name = s.Name ?? string.Empty,
        Price = s.Price ?? 0,
        UnitId = s.UnitId
    }).ToList();
}

// Review Queries
public async Task<List<PropertyReviewType>> GetReviewsByProperty(
    [Service] IReviewRepository repository,
    int propertyId,
    ReviewFilterOption filterOption,
    PageInput page)
{
    var pageRequest = new PageRequest
    {
        PageNumber = page.PageNumber,
        PageSize = page.PageSize
    };

    var filterOptions = filterOption switch
    {
        ReviewFilterOption.Newest => ReviewFilterOptions.Newest,
        ReviewFilterOption.Oldest => ReviewFilterOptions.Oldest,
        ReviewFilterOption.HighestRating => ReviewFilterOptions.HighestRating,
        ReviewFilterOption.LowestRating => ReviewFilterOptions.LowestRating,
        _ => ReviewFilterOptions.Newest
    };

    var reviews = await repository.GetReviewsByPropertyIdAsync(propertyId, pageRequest, filterOptions);

    return reviews.Select(r => MapToPropertyReviewType(r.PropertyReview, r.User)).ToList();
}

public async Task<List<PropertyReviewType>> GetReviewsByUser(
    [Service] IReviewRepository repository,
    int userId,
    ReviewFilterOption filterOption,
    PageInput page)
{
    var pageRequest = new PageRequest
    {
        PageNumber = page.PageNumber,
        PageSize = page.PageSize
    };

    var filterOptions = filterOption switch
    {
        ReviewFilterOption.Newest => ReviewFilterOptions.Newest,
        ReviewFilterOption.Oldest => ReviewFilterOptions.Oldest,
        ReviewFilterOption.HighestRating => ReviewFilterOptions.HighestRating,
        ReviewFilterOption.LowestRating => ReviewFilterOptions.LowestRating,
        _ => ReviewFilterOptions.Newest
    };

    var reviews = await repository.GetReviewsByUserIdAsync(userId, pageRequest, filterOptions);

    return reviews.Select(r => MapToPropertyReviewType(r.PropertyReview, r.User)).ToList();
}

public async Task<PropertyReviewType?> GetReviewById(
    [Service] IReviewRepository repository,
    int reviewId)
{
    var review = await repository.GetReviewByIdAsync(reviewId);
    if (review == null) return null;

    return MapToPropertyReviewType(review.Value.PropertyReview, review.Value.User);
}

// Response Queries
public async Task<PropertyReviewResponseType?> GetResponseById(
    [Service] IResponseRepository repository,
    int responseId)
{
    var response = await repository.GetPropertyReviewByIdAsync(responseId);
    if (response == null) return null;

    return MapToPropertyReviewResponseType(response.Value.Response, response.Value.User);
}

// Stay Queries
public async Task<List<StayType>> GetStays(
    [Service] IStayRepository repository,
    int userId,
    PeriodInput period,
    PageInput page)
{
    var periodRequest = new PeriodRequest { From = period.From, To = period.To };
    var pageRequest = new PageRequest
    {
        PageNumber = page.PageNumber,
        PageSize = page.PageSize
    };

    var stays = await repository.GetStays(userId, periodRequest, pageRequest);

    return stays.Select(s => new StayType
    {
        Id = s.Stay.Id,
        PropertyName = s.Property,
        UnitName = s.Unit,
        From = s.Stay.From,
        To = s.Stay.To,
        Status = s.Stay.Status.ToString()
    }).ToList();
}

public async Task<StayType?> GetStayDetails(
    [Service] IStayRepository repository,
    int stayId)
{
    var (stay, property, unit) = await repository.GetStayById(stayId);
    if (stay == null) return null;

    return new StayType
    {
        Id = stay.Id,
        PropertyName = property,
        UnitName = unit,
        From = stay.From,
        To = stay.To,
        Status = stay.Status.ToString()
    };
}

// Helper Methods
private PropertyReviewType MapToPropertyReviewType(PropertyReview review, User user)
{
    return new PropertyReviewType
    {
        Id = review.Id,
        Rating = review.Rating,
        Comment = review.Comment,
        CreatedAt = review.CreatedAt,
        User = new UserInfoType
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            PfpUrl = user.PfpUrl
        },
        Property = new PropertyInfoType
        {
            Id = review.Property.Id,
            Name = review.Property.Name,
            PictureUrl = review.Property.Pictures.FirstOrDefault(p => p.IsCover)?.Url
        }
    };
}

private PropertyReviewResponseType MapToPropertyReviewResponseType(PropertyReviewResponse response, User user)
{
    return new PropertyReviewResponseType
    {
        Id = response.Id,
        Comment = response.Comment,
        CreatedAt = response.CreatedAt,
        PropertyReviewId = response.PropertyReviewId,
        User = new UserInfoType
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            PfpUrl = user.PfpUrl
        },
        Property = new PropertyInfoType
        {
            Id = response.Property.Id,
            Name = response.Property.Name,
            PictureUrl = response.Property.Pictures.FirstOrDefault(p => p.IsCover)?.Url
        }
    };
}
*/
}