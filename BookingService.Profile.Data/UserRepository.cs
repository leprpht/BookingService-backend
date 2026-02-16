using BookingService.Database;
using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public class UserRepository(BookingServiceDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }
}