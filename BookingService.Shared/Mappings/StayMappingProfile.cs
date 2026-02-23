using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;

namespace BookingService.Shared.Mappings;

public class StayMappingProfile : AutoMapper.Profile
{
    public StayMappingProfile()
    {
        CreateMap<StayCreationDto, Stay>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => Enum.Parse<StayStatus>(s.Status, true)))
            .ForMember(d => d.RoomInstanceId, o => o.Ignore()); // assigned by service

        CreateMap<StayUpdateDto, Stay>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => Enum.Parse<StayStatus>(s.Status, true)));

        CreateMap<Stay, StayDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.PropertyName, o => o.MapFrom(s => s.RoomInstance.Unit.Property.Name))
            .ForMember(d => d.UnitName, o => o.MapFrom(s => s.RoomInstance.Unit.Name))
            .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomInstance.RoomNumber));
    }
}