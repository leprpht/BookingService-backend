using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
}