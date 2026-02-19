using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IReviewRepository : IBaseRepository<PropertyReview>
{
    Task UpdateCommentAsync(Guid id, Guid userId, string comment);
}