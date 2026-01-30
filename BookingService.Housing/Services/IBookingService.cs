using BookingService.Housing.DTOs;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public interface IBookingService
{
    Task<StayDto?> GetStayById(int id);
    Task<List<StayDto>> GetStaysByLocationId(int id, PeriodRequest period);
    Task<List<StayDto>> GetStaysByUserId(int id, PeriodRequest period);
}