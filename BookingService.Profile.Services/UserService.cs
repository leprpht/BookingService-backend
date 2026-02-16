using AutoMapper;
using BookingService.Profile.Data;
using BookingService.Profile.Dtos;
using BookingService.Shared.Extensions;

namespace BookingService.Profile.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);
        return user?.ToUserDto(mapper);
    }
}