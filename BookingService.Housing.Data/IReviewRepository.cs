using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IReviewRepository : IBaseRepository<PropertyReview>
{
    Task UpdateCommentAsync(int id, string comment);
}