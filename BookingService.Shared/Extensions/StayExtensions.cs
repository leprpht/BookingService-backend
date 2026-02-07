using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;

namespace BookingService.Shared.Extensions;

public static class StayExtensions
{
    public static Stay ToStay(this StayCreationDto dto, IMapper mapper)
    {
        return mapper.Map<Stay>(dto);
    }

    public static Stay ToStay(this StayUpdateDto dto, IMapper mapper)
    {
        return mapper.Map<Stay>(dto);
    }

    public static StayDto ToStayDto(this Stay stay, string property, string unit, IMapper mapper)
    {
        var dto = mapper.Map<StayDto>(stay);
        dto.PropertyName = property;
        dto.UnitName = unit;
        dto.Status = stay.Status.StayStatusToString();
        return dto;
    }

    private static string StayStatusToString(this StayStatus status)
    {
        return status switch
        {
            StayStatus.Pending => "Pending",
            StayStatus.Confirmed => "Confirmed",
            StayStatus.Cancelled => "Cancelled",
            StayStatus.Completed => "Completed",
            _ => "Unknown"
        };
    }
}