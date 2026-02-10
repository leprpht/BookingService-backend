namespace BookingService.Auth.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(int id, string email);
}