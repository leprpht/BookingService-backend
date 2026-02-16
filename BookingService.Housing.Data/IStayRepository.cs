using BookingService.Housing.Models;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;

namespace BookingServices.Housing.Data;

public interface IStayRepository : IBaseRepository<Stay>
{
    Task UpdateStatusAsync(int stayId, StayStatus status);
}