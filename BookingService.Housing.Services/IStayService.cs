using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IStayService : IBaseService<Stay, StayCreationDto, StayUpdateDto>
{
    Task UpdateStatusAsync(int stayId, int userId, StayStatus status);
}