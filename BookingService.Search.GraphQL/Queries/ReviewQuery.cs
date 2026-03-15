using BookingService.Database;
using BookingService.Search.GraphQL.Types.Review;

namespace BookingService.Search.GraphQL.Queries;

[ExtendObjectType("Query")]
public class ReviewQuery
{
    [UseFiltering]
    [UseSorting]
    public IQueryable<PropertyReviewType> GetPropertyReviews(
        [Service] BookingServiceDbContext context,
        Guid propertyId)
    {
        return context.PropertyReviews
            .Where(p => p.PropertyId == propertyId)
            .Select(r => new PropertyReviewType
            {
                Id = r.Id,
                PropertyId = r.PropertyId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                PropertyReviewResponses = r.PropertyReviewResponses
                    .Select(resp => new PropertyReviewResponseType 
                    { 
                        Id = resp.Id,
                        PropertyReviewId = resp.PropertyReviewId,
                        UserId = resp.UserId,
                        Comment = resp.Comment,
                        CreatedAt = resp.CreatedAt 
                    })
                    .OrderByDescending(resp => resp.CreatedAt)
                    .ToList()
            })
            .OrderByDescending(r => r.CreatedAt);
    }
}