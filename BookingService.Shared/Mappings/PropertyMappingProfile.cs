using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Mappings;

public class PropertyMappingProfile : AutoMapper.Profile
{
    public PropertyMappingProfile()
    {
        CreateMap<Property, PropertyListingDto>()
            .ForMember(d => d.Pictures,
                o => o.MapFrom(s =>
                    s.Pictures
                        .OrderByDescending(p => p.IsCover)
                        .Select(p => p.Url)))
            .ForMember(d => d.Units,
                o => o.MapFrom(s => s.Units))
            .ForMember(d => d.Rating,
                o => o.MapFrom(s =>
                    s.Reviews.Any() ? s.Reviews.Average(r => r.Rating) : 0));
        
        CreateMap<Property, PropertyPageDto>()
            .ForMember(d => d.Price,
                o => o.MapFrom((src, _, _, ctx) =>
                {
                    var period = (PeriodRequest)ctx.Items["Period"];
                    return src.Units.Min(u => u.Price) * period.DaysCount;
                }))
            .ForMember(d => d.PictureUrl,
                o => o.MapFrom(s =>
                    s.Pictures.FirstOrDefault(p => p.IsCover)!.Url))
            .ForMember(d => d.Rating,
                o => o.MapFrom(s => s.AverageRating));

        CreateMap<PropertyCreationDto, Property>();
        
        CreateMap<PropertyUpdateDto, Property>();

        CreateMap<Property, PropertyInfoDto>()
            .ForMember(d => d.PictureUrl,
                o => o.MapFrom(s =>
                    s.Pictures.FirstOrDefault(p => p.IsCover)!.Url));

        CreateMap<PropertyReviewCreationDto, PropertyReview>()
            .ForMember(d => d.CreatedAt,
                o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<PropertyReviewUpdateDto, PropertyReview>();

        CreateMap<PropertyReview, PropertyReviewDto>()
            .ForMember(d => d.Guest, o => o.Ignore())
            .ForMember(d => d.Property, o => o.MapFrom(s => s.Property));
    }
}
