using AutoMapper;
using BookingService.Profile.Data;
using BookingService.Profile.Dtos;
using BookingService.Shared.Extensions;
using BookingService.Shared.Utils;

namespace BookingService.Profile.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);
        return user?.ToUserDto(mapper);
    }

    public async Task UpdateUserNameAsync(int id, UserNameUpdate userNameUpdate) =>
        await repository.UpdateUserNameAsync(id, userNameUpdate.FirstName, userNameUpdate.MiddleName, userNameUpdate.LastName);

    public async Task UpdateUserEmailAsync(int id, string email)
    {
        if (!EmailValidator.IsValidEmail(email))
            throw new FormatException("Invalid email format.");
        
        await repository.UpdateUserEmailAsync(id, email);
    }
}