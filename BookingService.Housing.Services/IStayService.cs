using BookingService.Housing.DTOs.Stay;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public interface IStayService
{
    Task<List<StayDto>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest);
    Task<StayDto?> GetStayById(int stayId);
    Task CreateStayAsync(StayCreationDto stayCreationDto);
    Task UpdateStayAsync(StayUpdateDto stayUpdateDto);
    Task DeleteStayAsync(int id);
}