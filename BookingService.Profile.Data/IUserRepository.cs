using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int guestId, UserSearchType type);
    Task CreateUserAsync(User user, UserSearchType type);
    Task UpdateUserAsync(User user, UserSearchType type);
    Task DeleteUserAsync(int id, UserSearchType type);
}