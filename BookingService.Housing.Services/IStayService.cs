using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IStayService : IBaseService<StayCreationDto, StayUpdateDto>
{
    Task UpdateStatusAsync(int stayId, StayStatus status);
}