using BookingService.Database;
using BookingService.Profile.Model;
using BookingService.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Profile.Data;

public class UserRepository(BookingServiceDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }
    
    public async Task UpdateUserAsync(Guid id, string firstName, string? middleName, string lastName, string? pfpUrl, DateOnly dateOfBirth)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException();
        
        user.FirstName = firstName;
        user.MiddleName = middleName;
        user.LastName = lastName;
        user.ProfilePictureUrl = pfpUrl;
        user.DateOfBirth = dateOfBirth;
        
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserNameAsync(Guid id, string firstName, string? middleName, string lastName)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException();
        
        user.FirstName = firstName;
        user.MiddleName = middleName;
        user.LastName = lastName;
        
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserEmailAsync(Guid id, string email)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User not found.");
        
        var emailUnique = !await context.Users.AnyAsync(u => u.Email == email && u.Id != id);
        if (!emailUnique)
            throw new DuplicateEntityException("Email is already in use by another user.");
        
        user.Email = email;
        
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateUserProfilePictureAsync(Guid id, string pfpUrl)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException();
        
        user.ProfilePictureUrl = pfpUrl;
        
        await context.SaveChangesAsync();
    }
}