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
                    $"{(string.IsNullOrEmpty(s.MiddleName)
                        ? $"{s.FirstName } {s.LastName}" 
                        : $"{s.FirstName } {s.MiddleName} {s.LastName}")}"));

        CreateMap<User, UserDto>();
    }
}