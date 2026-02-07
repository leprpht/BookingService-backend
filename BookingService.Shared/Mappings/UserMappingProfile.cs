using AutoMapper;
using BookingService.Profile.Dtos;
using BookingService.Profile.Model;

namespace BookingService.Shared.Mappings;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(d => d.FullName,
                o => o.MapFrom(s =>
                    $"{s.FirstName.Trim()} {s.LastName.Trim()}"));

        CreateMap<UserCreationDto, User>();

        CreateMap<User, UserDto>();
    }
}