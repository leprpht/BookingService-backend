using BookingService.Housing.DTOs.Stay;
using BookingService.Shared.Requests;

namespace BookingService.Housing.Services;

public interface IStayService
{
    Task<List<StayDto>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest);
    Task<StayDto?> GetStayByIdAsync(int stayId);
    Task CreateStayAsync(StayCreationDto stayCreationDto);
    Task UpdateStayAsync(StayUpdateDto stayUpdateDto);
    Task DeleteStayAsync(int id);
}