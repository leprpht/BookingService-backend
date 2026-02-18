using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IStayRepository : IBaseRepository<Stay>
{
    Task<Stay?> GetByUserIdAsync(int userId);
    Task UpdateStatusAsync(int stayId, StayStatus status);
}