using BookingService.Housing.Models;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IReviewRepository
{
    Task<List<PropertyReview>> GetReviewsByPropertyIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions);
    Task<List<PropertyReview>> GetReviewsByUserIdAsync(int id, PageRequest pageRequest);
    Task<PropertyReview?> GetReviewByIdAsync(int reviewId);
    Task CreateReviewAsync(PropertyReview review);
    Task DeleteReviewAsync(int reviewId);
}