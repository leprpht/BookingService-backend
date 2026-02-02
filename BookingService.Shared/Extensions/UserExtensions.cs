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
}