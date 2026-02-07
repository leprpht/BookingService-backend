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

    public static User ToUser(this UserCreationDto dto, IMapper mapper)
    {
        return mapper.Map<User>(dto);
    }

    public static User ToUser(this UserUpdateDto dto, User existingUser, IMapper mapper)
    {
        var user = mapper.Map(dto, existingUser);
        return user;
    }
}