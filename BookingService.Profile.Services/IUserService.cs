using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task UpdateUserAsync(Guid id, UserInfoUpdate userUpdate);
    Task UpdateUserNameAsync(Guid id, UserNameDto userNameUpdate);
    Task UpdateUserEmailAsync(Guid id, string userEmailUpdate);
    Task UpdateProfilePictureAsync(Guid id, string pfpUrl);
}