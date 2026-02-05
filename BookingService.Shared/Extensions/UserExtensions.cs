using BookingService.Profile.Dtos;
using BookingService.Profile.Model;

namespace BookingService.Shared.Extensions;

public static class UserExtensions
{
    public static UserInfoDto ToUserInfoDto(this User user)
    {
        return new UserInfoDto
        {
            Id = user.Id,
            FullName = $"{user.FirstName.Trim()} {user.LastName.Trim()}",
            PfpUrl = user.PfpUrl
        };
    }

    public static User ToUser(this UserCreationDto user)
    {
        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            PfpUrl = user.PfpUrl
        };
    }

    public static User ToUser(this UserUpdateDto user, User existingUser)
    {
        return new User
        {
            Id = existingUser.Id,
            FirstName = user.FirstName ?? existingUser.FirstName,
            LastName = user.LastName ?? existingUser.LastName,
            Email = user.Email ?? existingUser.Email,
            Password = user.Password ?? existingUser.Password,
            PfpUrl = user.PfpUrl ?? existingUser.PfpUrl
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PfpUrl = user.PfpUrl
        };
    }
}