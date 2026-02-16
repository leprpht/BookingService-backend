using BookingService.Profile.Model;
using BookingService.Shared.Infrastructure.Repository;

namespace BookingService.Auth.Data;

public interface IAuthRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    new Task<int> AddAsync(User entity);
}