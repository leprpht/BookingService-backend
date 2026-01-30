using BookingService.Housing.Models;
using BookingService.Shared;

namespace BookingService.Housing.Data;

public interface IBookingRepository
{
    Task<Stay?> GetStayById(int id);
    Task<List<Stay>> GetStaysByLocationId(int id, PeriodRequest period);
    Task<List<Stay>> GetStaysByUserId(int id, PeriodRequest period);
}