using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IReviewRepository : IBaseRepository<PropertyReview>
{
    Task<List<(PropertyReview PropertyReview, User User)>> GetReviewsByPropertyIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions);
    Task<List<(PropertyReview PropertyReview, User User)>> GetReviewsByUserIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions);
    Task<(PropertyReview PropertyReview, User User)?> GetReviewByIdAsync(int reviewId);
    Task UpdateCommentAsync(int id, string comment);
}