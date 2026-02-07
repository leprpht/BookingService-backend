using BookingService.Housing.DTOs.Property;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IReviewService : IBaseService<PropertyReviewCreationDto, PropertyReviewUpdateDto>
{
    Task<List<PropertyReviewDto>> GetReviewsByPropertyIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest);
    Task<List<PropertyReviewDto>> GetReviewsByUserIdAsync(int id, ReviewFilterOptions filterOptions, PageRequest pageRequest);
    Task<PropertyReviewDto?> GetReviewById(int reviewId);
    Task AddReviewResponseAsync(int reviewId, string response);
}