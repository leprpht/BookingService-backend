using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Utils;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public class BookingService(IBookingRepository repository) : IBookingService
{
    public async Task<StayDto?> GetStayById(int id)
    {
        var stay = await repository.GetStayById(id);
        if (stay is null)
            return null;
        return stay.ToStayDto();
    }

    public async Task<List<StayDto>> GetStaysByLocationId(int id, PeriodRequest period)
    {
        var stays = await repository.GetStaysByLocationId(id, period);
        return stays
            .Select(s => s.ToStayDto())
            .ToList();
    }

    public async Task<List<StayDto>> GetStaysByUserId(int id, PeriodRequest period)
    {
        var stays = await repository.GetStaysByUserId(id, period);
        return stays
            .Select(s => s.ToStayDto())
            .ToList();
    }
}