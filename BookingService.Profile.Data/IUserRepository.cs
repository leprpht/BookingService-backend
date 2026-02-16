using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task UpdateUserNameAsync(int id, string firstName, string? middleName, string lastName);
}