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

    public static Stay ToStay(this StayUpdateDto dto, Guid userId, IMapper mapper) =>
        mapper.Map<Stay>(dto);

    public static StayDto ToStayDto(this Stay stay, IMapper mapper) =>
        mapper.Map<StayDto>(stay);

    private static string StayStatusToString(this StayStatus status) => status switch
    {
        StayStatus.Pending => "Pending",
        StayStatus.Confirmed => "Confirmed",
        StayStatus.Cancelled => "Cancelled",
        StayStatus.Completed => "Completed",
        _ => "Unknown"
    };
}