using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;

namespace BookingService.Shared.Extensions;

public static class StayExtensions
{
    public static Stay ToStay(this StayCreationDto dto, Guid userId, IMapper mapper)
    {
        var stay = mapper.Map<Stay>(dto);
        stay.UserId = userId;
        return stay;
    }

    public static Stay ToStay(this StayUpdateDto dto, Guid userId, IMapper mapper)
    {
        return mapper.Map<Stay>(dto);
    }
    
    public static StayDto ToStayDto(this Stay stay, IMapper mapper)
    {
        var dto = mapper.Map<StayDto>(stay);
        dto.PropertyName = stay.Unit.Property.Name;
        dto.UnitName = stay.Unit.Name;
        dto.Status = stay.Status.StayStatusToString();
        return dto;
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