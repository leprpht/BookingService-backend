using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IReviewRepository : IBaseRepository<PropertyReview>
{
    Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByPropertyIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions);
    Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByUserIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions);
    Task<(PropertyReview PropertyReview, Guest Guest)?> GetReviewByIdAsync(int reviewId);
    Task AddReviewResponseAsync(int reviewId, string response);
}