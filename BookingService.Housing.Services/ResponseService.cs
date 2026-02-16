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
    protected override PropertyReviewResponse MapCreate(PropertyReviewResponseCreationDto dto) => dto.ToPropertyReviewResponse(mapper);
    protected override PropertyReviewResponse MapUpdate(PropertyReviewResponseUpdateDto dto) => dto.ToPropertyReviewResponse(mapper);
    
    public async Task UpdateCommentAsync(int id, string comment) =>
        await repository.UpdateCommentAsync(id, comment);
}