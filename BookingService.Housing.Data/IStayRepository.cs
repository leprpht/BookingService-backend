using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IStayRepository : IBaseRepository<Stay>
{
    Task<List<(Stay Stay, string Property, string Unit)>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest);
    Task<(Stay? Stay, string Property, string Unit)> GetStayById(int stayId);
}