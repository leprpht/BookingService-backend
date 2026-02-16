using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id);
}