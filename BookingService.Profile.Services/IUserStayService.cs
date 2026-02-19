using BookingService.Housing.DTOs.Stay;
using BookingService.Profile.Model;
using BookingService.Shared.Requests;

namespace BookingService.Profile.Services;

public interface IUserStayService
{
    Task<List<StayDto>> GetUserStaysAsync(Guid userId, PageRequest pageRequest, StaySearchFilter filter);
}