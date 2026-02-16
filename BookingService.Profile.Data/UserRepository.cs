using BookingService.Database;
using BookingService.Profile.Model;

namespace BookingService.Profile.Data;

public class UserRepository(BookingServiceDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task UpdateUserNameAsync(int id, string firstName, string? middleName, string lastName)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            return;
        
        user.FirstName = firstName;
        user.MiddleName = middleName;
        user.LastName = lastName;
        
        await context.SaveChangesAsync();
    }
}