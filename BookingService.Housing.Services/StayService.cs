using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class StayService(IStayRepository repository, IMapper mapper)
    : BaseService<Stay, StayCreationDto, StayUpdateDto>(repository), IStayService
{
    protected override Stay MapCreate(StayCreationDto dto) => dto.ToStay(mapper);
    protected override Stay MapUpdate(StayUpdateDto dto) => dto.ToStay(mapper);
    
    public async Task<List<StayDto>> GetStays(int userId, PeriodRequest periodRequest, PageRequest pageRequest)
    {
        var stays = await repository.GetStays(userId, periodRequest, pageRequest);
        return stays
            .Select(s => s.Stay.ToStayDto(s.Property, s.Unit, mapper))
            .ToList();
    }

    public async Task<StayDto?> GetStayByIdAsync(int stayId)
    {
        var (stay, property, unit) =  await repository.GetStayById(stayId);
        return stay?.ToStayDto(property, unit, mapper);
    }
    
    public async Task UpdateStatusAsync(int stayId, StayStatus status) =>
        await repository.UpdateStatusAsync(stayId, status);
}