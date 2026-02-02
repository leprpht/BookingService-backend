using BookingService.Housing.Models;
using BookingService.Shared;

namespace BookingServices.Housing.Data;

public interface IStayRepository
{
    Task<List<(Stay Stay, string Property, string Unit)>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest);
    Task<(Stay? Stay, string Property, string Unit)> GetStayById(int stayId);
    Task CreateStayAsync(Stay stay);
    Task UpdateStayAsync(Stay stay);
    Task DeleteStayAsync(int id);
}