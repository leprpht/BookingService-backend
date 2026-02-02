using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;

namespace BookingService.Shared;

public static class StayExtensions
{
    public static Stay ToStay(this StayCreationDto stayCreationDto)
    {
        return new Stay
        {
            UnitId = stayCreationDto.UnitId,
            GuestId = stayCreationDto.GuestId,
            From = stayCreationDto.From,
            To = stayCreationDto.To,
            Status = Enum.Parse<StayStatus>(stayCreationDto.Status.ToUpper())
        };
    }

    public static Stay ToStay(this StayUpdateDto stayUpdateDto)
    {
        return new Stay
        {
            Id = stayUpdateDto.Id,
            UnitId = stayUpdateDto.UnitId,
            GuestId = stayUpdateDto.GuestId,
            From = stayUpdateDto.From,
            To = stayUpdateDto.To,
            TotalPrice = stayUpdateDto.TotalPrice,
            Status = Enum.Parse<StayStatus>(stayUpdateDto.Status.ToUpper())
        };
    }

    public static StayDto ToStayDto(this Stay stay, string property, string unit)
    {
        return new StayDto
        {
            Id = stay.Id,
            PropertyName = property,
            UnitName = unit,
            From = stay.From,
            To = stay.To,
            Status = stay.Status.StayStatusToString()
        };
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