using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;

namespace BookingService.Housing.Services;

public interface IReviewService
{
    Task<List<PropertyReviewDto>> GetReviewsByPropertyIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest);
    Task<List<PropertyReviewDto>> GetReviewsByUserIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest);
    Task<PropertyReviewDto?> GetReviewById(int reviewId);
    Task CreateReviewAsync(PropertyReviewCreationDto propertyReviewCreationDto);
    Task DeleteReviewAsync(int id);
}