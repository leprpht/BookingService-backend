using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task UpdateUserAsync(int id, string firstName, string? middleName, string lastName, string? pfpUrl, DateOnly dateOfBirth);
    Task UpdateUserNameAsync(int id, string firstName, string? middleName, string lastName);
    Task UpdateUserEmailAsync(int id, string email);
    Task UpdateUserProfilePictureAsync(int id, string pfpUrl);
}