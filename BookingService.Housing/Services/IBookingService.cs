using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public interface IBookingService
{
    Task<Stay?> GetStayById(int id);
    Task<List<Stay>> GetStaysByLocationId(int id);
}