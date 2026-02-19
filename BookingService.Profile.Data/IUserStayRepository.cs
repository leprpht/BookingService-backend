using BookingService.Housing.Models;
using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserStayRepository
{
    Task<List<Stay>> GetUserStays(Guid userId, int page, int pageSize, StaySearchFilter filter);
}