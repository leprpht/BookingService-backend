using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class ReviewService(IReviewRepository repository, IMapper mapper)
    : BaseService<PropertyReview, PropertyReviewCreationDto, PropertyReviewUpdateDto>(repository), IReviewService
{
    protected override PropertyReview MapCreate(PropertyReviewCreationDto dto) => dto.ToPropertyReview(mapper);
    protected override PropertyReview MapUpdate(PropertyReviewUpdateDto dto) => dto.ToPropertyReview(mapper);
    
    public async Task<List<PropertyReviewDto>> GetReviewsByPropertyIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest)
    {
        var reviews = await repository.GetReviewsByPropertyIdAsync(id, pageRequest, filterOptions);
        return reviews.Select(r => r.PropertyReview.ToPropertyReviewDto(r.Guest, mapper)).ToList();
    }

    public async Task<List<PropertyReviewDto>> GetReviewsByUserIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest)
    {
        var reviews = await repository.GetReviewsByUserIdAsync(id, pageRequest, filterOptions);
        return reviews.Select(r => r.PropertyReview.ToPropertyReviewDto(r.Guest, mapper)).ToList();
    }

    public async Task<PropertyReviewDto?> GetReviewById(int reviewId)
    {
        var review = await repository.GetReviewByIdAsync(reviewId);
        return review?.PropertyReview.ToPropertyReviewDto(review.Value.Guest, mapper);
    }

    public async Task AddReviewResponseAsync(int reviewId, string response)
    {
        await repository.AddReviewResponseAsync(reviewId, response);
    }
}