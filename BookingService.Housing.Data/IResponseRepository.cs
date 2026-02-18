using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IResponseRepository : IBaseRepository<PropertyReviewResponse>
{
    Task UpdateCommentAsync(int id, int userId, string comment);
}