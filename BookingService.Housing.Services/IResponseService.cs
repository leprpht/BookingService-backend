using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IResponseService : IBaseService<PropertyReviewResponseCreationDto, PropertyReviewResponseUpdateDto>
{
    Task UpdateCommentAsync(int id, string comment);
}