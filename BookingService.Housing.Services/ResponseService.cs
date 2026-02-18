using AutoMapper;
using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class ResponseService(IResponseRepository repository, IMapper mapper)
    : BaseService<PropertyReviewResponse, PropertyReviewResponseCreationDto, PropertyReviewResponseUpdateDto>(repository), IResponseService
{
    protected override PropertyReviewResponse MapCreate(int userId, PropertyReviewResponseCreationDto dto) => dto.ToPropertyReviewResponse(userId, mapper);
    protected override PropertyReviewResponse MapUpdate(int userId, PropertyReviewResponseUpdateDto dto) => dto.ToPropertyReviewResponse(userId, mapper);
    
    public async Task UpdateCommentAsync(int id, int userId, string comment) =>
        await repository.UpdateCommentAsync(id, userId, comment);
}