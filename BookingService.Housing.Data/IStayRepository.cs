using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingServices.Housing.Data;

public interface IStayRepository : IBaseRepository<Stay>
{
    Task<List<Stay>> GetByUserIdAsync(Guid userId);
    Task UpdateStatusAsync(Guid stayId, StayStatus status);
}