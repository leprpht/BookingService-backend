using BookingService.Housing.DTOs.Stay;
using BookingService.Shared;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class StayService(IStayRepository repository) : IStayService
{
    public async Task<List<StayDto>> GetStays(int guestId, PeriodRequest periodRequest, PageRequest pageRequest)
    {
        var stays = await repository.GetStays(guestId, periodRequest, pageRequest);
        return stays
            .Select(s => s.Stay.ToStayDto(s.Property, s.Unit))
            .ToList();
    }

    public async Task<StayDto?> GetStayByIdAsync(int stayId)
    {
        var (stay, property, unit) =  await repository.GetStayById(stayId);
        return stay?.ToStayDto(property, unit);
    }

    public async Task CreateStayAsync(StayCreationDto stayCreationDto)
    {
        var stay = stayCreationDto.ToStay();
        await repository.CreateStayAsync(stay);
    }

    public async Task UpdateStayAsync(StayUpdateDto stayUpdateDto)
    {
        var stay = stayUpdateDto.ToStay();
        await repository.UpdateStayAsync(stay);
    }

    public async Task DeleteStayAsync(int id)
    {
        await repository.DeleteStayAsync(id);
    }
}