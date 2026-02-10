using BookingService.Database;
using BookingService.Profile.Model;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Auth.Data;

public class AuthRepository(BookingServiceDbContext context)
    : BaseRepository<User>(context), IAuthRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DbSet.SingleOrDefaultAsync(u => u.Email == email);
    }
    
    public new async Task<int> AddAsync(User entity)
    {
        var entry = await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entry.Entity.Id;
    }
}