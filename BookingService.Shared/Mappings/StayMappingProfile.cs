using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;

namespace BookingService.Shared.Mappings;

public class StayMappingProfile : AutoMapper.Profile
{
    public StayMappingProfile()
    {
        CreateMap<StayCreationDto, Stay>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s =>
                    Enum.Parse<StayStatus>(s.Status, true)));

        CreateMap<StayUpdateDto, Stay>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s =>
                    Enum.Parse<StayStatus>(s.Status, true)));

        CreateMap<Stay, StayDto>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => s.Status.ToString()));
    }
}