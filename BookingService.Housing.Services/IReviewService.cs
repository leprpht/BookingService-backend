using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IReviewService : IBaseService<PropertyReview, PropertyReviewCreationDto, PropertyReviewUpdateDto>
{
    Task UpdateCommentAsync(int id, int userId, string comment);
}