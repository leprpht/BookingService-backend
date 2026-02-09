using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public interface IResponseRepository : IBaseRepository<PropertyReviewResponse>
{
    Task<(PropertyReviewResponse Response, Guest User)?> GetPropertyReviewByIdAsync(int id);
    Task UpdateCommentAsync(int id, string comment);
}