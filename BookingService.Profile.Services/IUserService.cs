using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id);
    Task UpdateUserNameAsync(int id, UserNameUpdate userNameUpdate);
}