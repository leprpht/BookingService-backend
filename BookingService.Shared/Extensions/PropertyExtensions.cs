using AutoMapper;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Profile.Dtos;
using BookingService.Profile.Model;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Extensions;

public static class PropertyExtensions
{
    public static PropertyListingDto ToPropertyListingDto(this Property property, PeriodRequest period, IMapper mapper)
    {
        return mapper.Map<PropertyListingDto>(property,opt => opt.Items["Period"] = period);
    }

    public static PropertyPageDto ToPropertyPageDto(this Property property, PeriodRequest period, IMapper mapper)
    {
        return mapper.Map<PropertyPageDto>(property, opt => opt.Items["Period"] = period);
    }

    public static Property ToProperty(this PropertyCreationDto dto, IMapper mapper)
    {
        return mapper.Map<Property>(dto);
    }

    public static Property ToProperty(this PropertyUpdateDto dto, IMapper mapper)
    {
        return mapper.Map<Property>(dto);
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewCreationDto dto, IMapper mapper)
    {
        return mapper.Map<PropertyReview>(dto);
    }

    public static PropertyReview ToPropertyReview(this PropertyReviewUpdateDto dto, IMapper mapper)
    {
        return mapper.Map<PropertyReview>(dto);
    }

    public static PropertyReviewDto ToPropertyReviewDto(this PropertyReview review, Guest guest, IMapper mapper)
    {
        var dto = mapper.Map<PropertyReviewDto>(review);
        dto.Guest = mapper.Map<UserInfoDto>(guest);
        return dto;
    }
}