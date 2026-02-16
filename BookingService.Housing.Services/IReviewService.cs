using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IReviewService : IBaseService<PropertyReviewCreationDto, PropertyReviewUpdateDto>
{
    Task UpdateCommentAsync(int id, string comment);
}