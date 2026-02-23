using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Mappings;

public class UnitMappingProfile : AutoMapper.Profile
{
    public UnitMappingProfile()
    {
        CreateMap<Unit, UnitListDto>()
            .ForMember(d => d.Price,
                o => o.MapFrom((src, _, _, ctx) =>
                {
                    var period = (PeriodRequest)ctx.Items["Period"];
                    return src.Price * period.DaysCount;
                }))
            .ForMember(d => d.IsAvailable,
                o => o.MapFrom((src, _, _, ctx) =>
                {
                    var period = (PeriodRequest)ctx.Items["Period"];
                    return !src.Rooms.Any(ri =>
                        ri.Stays
                            .Any(s => s.Status != StayStatus.Cancelled && s.From < period.To && s.To > period.From));
                }));

        CreateMap<Unit, UnitDto>()
            .ForMember(d => d.Price,
                o => o.MapFrom((src, _, _, ctx) =>
                {
                    var period = (PeriodRequest)ctx.Items["Period"];
                    return src.Price * period.DaysCount;
                }))
            .ForMember(d => d.Customizations,
                o => o.MapFrom(s => s.Customizations))
            .ForMember(d => d.Pictures,
                o => o.MapFrom(s => s.Pictures.Select(p => p.Url)));

        CreateMap<UnitCreationDto, Unit>();
        CreateMap<UnitUpdateDto, Unit>();

        CreateMap<UnitCustomizationCreationDto, UnitCustomization>()
            .ForMember(d => d.UnitId, o => o.Ignore());

        CreateMap<UnitCustomizationUpdateDto, UnitCustomization>()
            .ForMember(d => d.UnitId, o => o.Ignore());

        CreateMap<UnitCustomization, UnitCustomizationGroupedDto>();

        CreateMap<ICollection<UnitCustomization>, List<UnitCustomizationDto>>()
            .ConvertUsing(src =>
                src.GroupBy(c => c.Type)
                   .Select(g => new UnitCustomizationDto
                   {
                       Type = g.Key.ToString(),
                       Customizations = g
                           .Select(c => new UnitCustomizationGroupedDto
                           {
                               Id = c.Id,
                               Text = c.Text
                           })
                           .ToList()
                   })
                   .ToList());
        
        CreateMap<UnitPictureCreationDto, UnitPicture>()
            .ForMember(d => d.UnitId, o => o.Ignore());
        
        CreateMap<UnitPictureUpdateDto, UnitPicture>()
            .ForMember(d => d.UnitId, o => o.Ignore());
        
        CreateMap<UnitAdditionalServices, UnitAdditionalServicesDto>();

        CreateMap<UnitAdditionalServicesCreationDto, UnitAdditionalServices>();

        CreateMap<UnitAdditionalServicesUpdateDto, UnitAdditionalServices>();
    }
}
