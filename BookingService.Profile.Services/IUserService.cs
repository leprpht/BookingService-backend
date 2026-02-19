using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id);
    Task UpdateUserAsync(int id, UserInfoUpdate userUpdate);
    Task UpdateUserNameAsync(int id, UserNameDto userNameUpdate);
    Task UpdateUserEmailAsync(int id, string userEmailUpdate);
    Task UpdateProfilePictureAsync(int id, string pfpUrl);
}