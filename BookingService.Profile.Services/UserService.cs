using AutoMapper;
using BookingService.Profile.Data;
using BookingService.Profile.Dtos;
using BookingService.Shared.Extensions;
using BookingService.Shared.Utils;

namespace BookingService.Profile.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id);
        return user?.ToUserDto(mapper);
    }

    public async Task UpdateUserAsync(Guid id, UserInfoUpdate userUpdate) =>
        await repository.UpdateUserAsync(
            id,
            userUpdate.Name.FirstName,
            userUpdate.Name.MiddleName,
            userUpdate.Name.LastName,
            userUpdate.ProfilePictureUrl,
            userUpdate.DateOfBirth);

    public async Task UpdateUserNameAsync(Guid id, UserNameDto userNameUpdate) =>
        await repository.UpdateUserNameAsync(id, userNameUpdate.FirstName, userNameUpdate.MiddleName, userNameUpdate.LastName);

    public async Task UpdateUserEmailAsync(Guid id, string email)
    {
        if (!EmailValidator.IsValidEmail(email))
            throw new FormatException("Invalid email format.");
        
        await repository.UpdateUserEmailAsync(id, email);
    }

    public Task UpdateProfilePictureAsync(Guid id, string pfpUrl) =>
        repository.UpdateUserProfilePictureAsync(id, pfpUrl);
}