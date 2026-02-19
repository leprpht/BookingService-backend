using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IResponseService : IBaseService<PropertyReviewResponse, PropertyReviewResponseCreationDto, PropertyReviewResponseUpdateDto>
{
    Task UpdateCommentAsync(Guid id, Guid userId, string comment);
}