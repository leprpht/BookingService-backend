using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Extensions;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class ReviewService(IReviewRepository repository) : IReviewService
{
    public async Task<List<PropertyReviewDto>> GetReviewsByPropertyIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest)
    {
        var reviews = await repository.GetReviewsByPropertyIdAsync(id, pageRequest, filterOptions);
        return reviews.Select(r => r.PropertyReview.ToPropertyReviewDto(r.Guest)).ToList();
    }

    public async Task<List<PropertyReviewDto>> GetReviewsByUserIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest)
    {
        var reviews = await repository.GetReviewsByUserIdAsync(id, pageRequest, filterOptions);
        return reviews.Select(r => r.PropertyReview.ToPropertyReviewDto(r.Guest)).ToList();
    }

    public async Task<PropertyReviewDto?> GetReviewById(int reviewId)
    {
        var review = await repository.GetReviewByIdAsync(reviewId);
        return review?.PropertyReview.ToPropertyReviewDto(review.Value.Guest);
    }

    public async Task CreateReviewAsync(PropertyReviewCreationDto propertyReviewCreationDto)
    {
        var review = propertyReviewCreationDto.ToPropertyReview();
        await repository.AddAsync(review);
    }

    public async Task AddReviewResponseAsync(int reviewId, string response)
    {
        await repository.AddReviewResponseAsync(reviewId, response);
    }

    public async Task DeleteReviewAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}