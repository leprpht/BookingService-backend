using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;

namespace BookingService.Housing.Services;

public interface IStayService : IBaseService<StayCreationDto, StayUpdateDto>
{
    Task<List<StayDto>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest);
    Task<StayDto?> GetStayByIdAsync(int stayId);
    Task UpdateStatusAsync(int stayId, StayStatus status);
}