using BookingService.Profile.Dtos;

namespace BookingService.Profile.Services;

public interface IGuestService
{
    Task<UserInfoDto?> GetGuestInfoAsync(int id);
    Task<UserDto?> GetGuestByIdAsync(int id);
    Task CreateGuestAsync(UserCreationDto guest);
    Task UpdateGuestAsync(UserUpdateDto guest);
    Task DeleteGuestAsync(int guestId);
}