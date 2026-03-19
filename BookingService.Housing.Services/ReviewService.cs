using AutoMapper;
using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class ReviewService(IReviewRepository repository, IMapper mapper)
    : BaseService<PropertyReview, PropertyReviewCreationDto, PropertyReviewUpdateDto>(repository), IReviewService
{
    protected override PropertyReview MapCreate(Guid userId, PropertyReviewCreationDto dto)
    {
        return dto.ToPropertyReview(userId, mapper);
    }

    protected override PropertyReview MapUpdate(Guid userId, PropertyReviewUpdateDto dto)
    {
        return dto.ToPropertyReview(userId, mapper);
    }

    public async Task UpdateCommentAsync(Guid id, Guid userId, string comment)
    {
        await repository.UpdateCommentAsync(id, userId, comment);
    }
}