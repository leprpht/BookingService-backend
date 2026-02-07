using AutoMapper;
using BookingService.Profile.Data;
using BookingService.Profile.Dtos;
using BookingService.Shared.Extensions;

namespace BookingService.Profile.Services;

public class GuestService(IUserRepository userRepository, IMapper mapper) : IGuestService
{
    public async Task<UserInfoDto?> GetGuestInfoAsync(int guestId)
    {
        var guest = await userRepository.GetUserByIdAsync(guestId, UserSearchType.Guest);
        return guest?.ToUserInfoDto(mapper);
    }

    public async Task<UserDto?> GetGuestByIdAsync(int guestId)
    {
        var guest =  await userRepository.GetUserByIdAsync(guestId, UserSearchType.Guest);
        return guest?.ToUserDto(mapper);
    }

    public async Task CreateGuestAsync(UserCreationDto guest)
    {
        var guestEntity = guest.ToUser(mapper);
        await userRepository.CreateUserAsync(guestEntity, UserSearchType.Guest);
    }

    public async Task UpdateGuestAsync(UserUpdateDto guest)
    {
        var existingGuest = await userRepository.GetUserByIdAsync(guest.Id, UserSearchType.Guest);
        if (existingGuest == null)
        {
            throw new Exception("Guest not found");
        }

        var updatedGuest = guest.ToUser(existingGuest, mapper);
        await userRepository.UpdateUserAsync(updatedGuest, UserSearchType.Guest);
    }

    public async Task DeleteGuestAsync(int guestId)
    {
        await userRepository.DeleteUserAsync(guestId, UserSearchType.Guest);
    }
}