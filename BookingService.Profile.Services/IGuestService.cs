using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IGuestService
{
    Task<UserInfoDto?> GetGuestInfoAsync(int guestId);
    Task<UserDto?> GetGuestByIdAsync(int userId);
    Task CreateGuestAsync(UserCreationDto guest);
    Task UpdateGuestAsync(UserUpdateDto guest);
    Task DeleteGuestAsync(int guestId);
}