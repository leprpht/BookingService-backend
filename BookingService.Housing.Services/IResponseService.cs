using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IResponseService : IBaseService<PropertyReviewResponseCreationDto, PropertyReviewResponseUpdateDto>
{
    Task UpdateCommentAsync(int id, string comment);
}