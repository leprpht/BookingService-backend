using AutoMapper;
using BookingService.Profile.Dtos;
using BookingService.Profile.Model;

namespace BookingService.Shared.Extensions;

public static class UserExtensions
{
    public static UserInfoDto ToUserInfoDto(this User user, IMapper mapper)
    {
        return mapper.Map<UserInfoDto>(user);
    }

    public static UserDto ToUserDto(this User user, IMapper mapper)
    {
        return mapper.Map<UserDto>(user);
    }
}