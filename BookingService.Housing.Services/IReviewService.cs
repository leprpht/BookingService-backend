using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IReviewService : IBaseService<PropertyReviewCreationDto, PropertyReviewUpdateDto>
{
    Task UpdateCommentAsync(int id, string comment);
}