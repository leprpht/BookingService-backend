using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task UpdateUserAsync(Guid id, string firstName, string? middleName, string lastName, string? pfpUrl, DateOnly dateOfBirth);
    Task UpdateUserNameAsync(Guid id, string firstName, string? middleName, string lastName);
    Task UpdateUserEmailAsync(Guid id, string email);
    Task UpdateUserProfilePictureAsync(Guid id, string pfpUrl);
}