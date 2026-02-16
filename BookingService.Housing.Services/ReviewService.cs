using AutoMapper;
using BookingService.Housing.DTOs.PropertyReview;
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
    
    public async Task UpdateCommentAsync(int id, string comment) =>
        await repository.UpdateCommentAsync(id, comment);
}