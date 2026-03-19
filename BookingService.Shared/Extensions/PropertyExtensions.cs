using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Models;
using BookingService.Profile.Dtos;
using BookingService.Profile.Model;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Extensions;

public static class PropertyExtensions
{
    public static PropertyListingDto ToPropertyListingDto(this Property property, PeriodRequest period, IMapper mapper)
    {
        return mapper.Map<PropertyListingDto>(property, opt => opt.Items["Period"] = period);
    }

    public static PropertyPageDto ToPropertyPageDto(this Property property, PeriodRequest period, IMapper mapper)
    {
        return mapper.Map<PropertyPageDto>(property, opt => opt.Items["Period"] = period);
    }

    public static Property ToProperty(this PropertyCreationDto dto, Guid userId, IMapper mapper)
    {
        var property = mapper.Map<Property>(dto);
        property.OwnerId = userId;
        return property;
    }

    public static Property ToProperty(this PropertyUpdateDto dto, Guid userId, IMapper mapper)
    {
        var property = mapper.Map<Property>(dto);
        property.OwnerId = userId;
        return property;
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewCreationDto dto, Guid userId, IMapper mapper)
    {
        var review = mapper.Map<PropertyReview>(dto);
        review.UserId = userId;
        return review;
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewUpdateDto dto, Guid userId, IMapper mapper)
    {
        return mapper.Map<PropertyReview>(dto);
    }

    public static PropertyReviewDto ToPropertyReviewDto(this PropertyReview review, User user, IMapper mapper)
    {
        var dto = mapper.Map<PropertyReviewDto>(review);
        dto.User = mapper.Map<UserInfoDto>(user);
        dto.Responses = review.PropertyReviewResponses
            .Select(r => r.ToPropertyReviewResponseDto(user, mapper))
            .OrderBy(r => r.CreatedAt)
            .ToList();
        return dto;
    }

    public static PropertyReviewResponse ToPropertyReviewResponse(this PropertyReviewResponseCreationDto dto,
        Guid userId, IMapper mapper)
    {
        var response = mapper.Map<PropertyReviewResponse>(dto);
        response.UserId = userId;
        return response;
    }

    public static PropertyReviewResponse ToPropertyReviewResponse(this PropertyReviewResponseUpdateDto dto, Guid userId,
        IMapper mapper)
    {
        var response = mapper.Map<PropertyReviewResponse>(dto);
        response.UserId = userId;
        return response;
    }

    public static PropertyReviewResponseDto ToPropertyReviewResponseDto(this PropertyReviewResponse response, User user,
        IMapper mapper)
    {
        var dto = mapper.Map<PropertyReviewResponseDto>(response);
        dto.User = mapper.Map<UserInfoDto>(user);
        return dto;
    }

    public static PropertyPicture ToPropertyPicture(this PropertyPictureCreationDto dto, Guid propertyId,
        IMapper mapper)
    {
        var picture = mapper.Map<PropertyPicture>(dto);
        picture.PropertyId = propertyId;
        return picture;
    }

    public static PropertyPicture ToPropertyPicture(this PropertyPictureUpdateDto dto, Guid propertyId, IMapper mapper)
    {
        var picture = mapper.Map<PropertyPicture>(dto);
        picture.PropertyId = propertyId;
        return picture;
    }
}