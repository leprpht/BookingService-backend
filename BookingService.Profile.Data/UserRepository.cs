using BookingService.Database;
using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Profile.Data;

public class UserRepository(BookingServiceDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(int guestId, UserSearchType type)
    {
        return type switch
        {
            UserSearchType.Guest => await context.Guests.SingleOrDefaultAsync(u => u.Id == guestId),
            UserSearchType.Owner => await context.Owners.SingleOrDefaultAsync(u => u.Id == guestId),
            _ => null
        };
    }

    public async Task CreateUserAsync(User user, UserSearchType type)
    {
        switch (type)
        {
            case UserSearchType.Guest:
                await context.Guests.AddAsync((Guest) user);
                break;
            case UserSearchType.Owner:
                await context.Owners.AddAsync((Owner) user);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user, UserSearchType type)
    {
        switch (type)
        {
            case UserSearchType.Guest:
                context.Guests.Update((Guest) user);
                break;
            case UserSearchType.Owner:
                context.Owners.Update((Owner) user);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id, UserSearchType type)
    {
        switch (type)
        {
            case UserSearchType.Guest:
                var guest = await context.Guests.FindAsync(id);
                if (guest != null)
                {
                    context.Guests.Remove(guest);
                }
                break;
            case UserSearchType.Owner:
                var owner = await context.Owners.FindAsync(id);
                if (owner != null)
                {
                    context.Owners.Remove(owner);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        await context.SaveChangesAsync();
    }
}